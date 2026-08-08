$ErrorActionPreference = 'Stop'

function Write-PolicyHookJson {
    param([Parameter(Mandatory = $true)] [object] $Value)
    $Value | ConvertTo-Json -Depth 16 -Compress
}

function Get-PolicySha256 {
    param([Parameter(Mandatory = $true)] [AllowEmptyString()] [string] $Value)
    $sha = [System.Security.Cryptography.SHA256]::Create()
    try {
        $bytes = [System.Text.Encoding]::UTF8.GetBytes($Value)
        $hash = $sha.ComputeHash($bytes)
        return ([System.BitConverter]::ToString($hash)).Replace('-', '').ToLowerInvariant()
    }
    finally { $sha.Dispose() }
}

function ConvertTo-PolicyNormalizedText {
    param([AllowEmptyString()] [string] $Value)
    if ($null -eq $Value) { return '' }
    return $Value.Replace('\', '/').ToLowerInvariant()
}

function Assert-PolicyRelativePath {
    param([Parameter(Mandatory = $true)] [string] $Path, [Parameter(Mandatory = $true)] [string] $Field)
    $normalized = ConvertTo-PolicyNormalizedText $Path
    if ([string]::IsNullOrWhiteSpace($normalized) -or
        [System.IO.Path]::IsPathRooted($Path) -or
        $normalized -eq '..' -or $normalized.StartsWith('../') -or $normalized.Contains('/../')) {
        throw "$Field must contain a repository-relative path without parent traversal: '$Path'."
    }
}

function Assert-PolicyDefinition {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    if ([string] $Policy.repository.writeScope -ne 'repository-only' -or [string] $Policy.repository.outsideRepository -ne 'ignore') {
        throw 'Policy repository scope must be repository-only with outsideRepository=ignore.'
    }
    if ([bool] $Policy.runtimePermissions.managedByRepository) {
        throw 'A repository policy cannot manage the Codex runtime permission profile.'
    }
    [void] (Get-PolicyRiskRank ([string] $Policy.riskClassification.default))

    $ruleNames = @{}
    foreach ($rule in @($Policy.rules)) {
        $name = [string] $rule.name
        if ([string]::IsNullOrWhiteSpace($name) -or $ruleNames.ContainsKey($name)) { throw "Policy rule names must be non-empty and unique: '$name'." }
        $ruleNames[$name] = $true
        if ([string] $rule.kind -notin @('generatedPathEdit', 'protectedPathEdit', 'directGenerator', 'commandRegex')) { throw "Unsupported policy rule kind '$($rule.kind)'." }
        if ([string] $rule.severity -notin @('deny', 'context')) { throw "Unsupported policy severity '$($rule.severity)'." }
        [void] (Get-PolicyRiskRank ([string] $rule.risk))
        if ([string] $rule.kind -eq 'commandRegex' -and [string]::IsNullOrWhiteSpace([string] $rule.pattern)) { throw "Rule '$name' requires a pattern." }
    }

    foreach ($path in @($Policy.generatedPaths) + @($Policy.generators.trusted) + @($Policy.generators.direct)) {
        Assert-PolicyRelativePath -Path ([string] $path) -Field 'generatedPaths/generators'
    }
    Assert-PolicyRelativePath -Path ([string] $Policy.verification.entryPoint) -Field 'verification.entryPoint'
    Assert-PolicyRelativePath -Path ([string] $Policy.verification.reviewEntryPoint) -Field 'verification.reviewEntryPoint'
    Assert-PolicyRelativePath -Path ([string] $Policy.verification.receiptPath) -Field 'verification.receiptPath'

    $checkNames = @{}
    foreach ($check in @($Policy.verification.checks)) {
        $name = [string] $check.name
        if ([string]::IsNullOrWhiteSpace($name) -or $checkNames.ContainsKey($name)) { throw "Verification check names must be non-empty and unique: '$name'." }
        $checkNames[$name] = $true
        if ([string]::IsNullOrWhiteSpace([string] $check.executable)) { throw "Verification check '$name' requires an executable." }
        Assert-PolicyRelativePath -Path ([string] $check.workingDirectory) -Field "verification.checks[$name].workingDirectory"
        if (-not [string]::IsNullOrWhiteSpace([string] $check.minimumRisk)) { [void] (Get-PolicyRiskRank ([string] $check.minimumRisk)) }
        if ([string] $check.executable -eq 'dotnet' -and @($check.arguments) -contains 'build' -and @($check.arguments) -notcontains '--no-dependencies') {
            throw "Dotnet build check '$name' must use --no-dependencies to preserve read-only sibling repositories."
        }
        foreach ($precondition in @($check.preconditions)) {
            if ($null -eq $precondition) { continue }
            if ([string] $precondition.kind -ne 'anyPathExists') { throw "Verification check '$name' has an unsupported precondition kind '$($precondition.kind)'." }
            foreach ($path in @($precondition.paths)) { Assert-PolicyRelativePath -Path ([string] $path) -Field "verification.checks[$name].preconditions.paths" }
        }
    }

    foreach ($dependency in @($Policy.dependencies)) {
        if ([string] $dependency.defaultAccess -ne 'read-only' -or [bool] $dependency.automaticVerification) {
            throw "Dependency '$($dependency.repository)' must remain read-only and cannot enable automatic verification from JCS."
        }
        if (-not [System.IO.Path]::IsPathRooted([string] $dependency.path)) { throw "Dependency '$($dependency.repository)' must declare an absolute path." }
    }
}

function Get-PolicyData {
    param([Parameter(Mandatory = $true)] [string] $PolicyPath)
    $resolved = (Resolve-Path -LiteralPath $PolicyPath).Path
    $policy = Get-Content -Raw -LiteralPath $resolved | ConvertFrom-Json
    if ([int] $policy.version -ne 2) {
        throw "Unsupported policy version in $resolved. Expected version 2."
    }
    if ([string]::IsNullOrWhiteSpace([string] $policy.repository.name) -or
        [string]::IsNullOrWhiteSpace([string] $policy.repository.root) -or
        [string]::IsNullOrWhiteSpace([string] $policy.verification.entryPoint)) {
        throw 'Policy must define repository.name, repository.root, and verification.entryPoint.'
    }
    Assert-PolicyDefinition -Policy $policy
    $actualRoot = (Resolve-Path -LiteralPath (Join-Path (Split-Path -Parent $resolved) '..')).Path.TrimEnd('\')
    $declaredRootValue = [string] $policy.repository.root
    $declaredRoot = if ([System.IO.Path]::IsPathRooted($declaredRootValue)) {
        (Resolve-Path -LiteralPath $declaredRootValue).Path.TrimEnd('\')
    }
    else {
        [System.IO.Path]::GetFullPath((Join-Path $actualRoot $declaredRootValue)).TrimEnd('\')
    }
    if (-not $declaredRoot.Equals($actualRoot, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Policy repository root '$declaredRoot' does not match its containing repository '$actualRoot'."
    }
    # Keep the declaration relocatable on disk while exposing a canonical root
    # to every consumer after loading.
    $policy.repository.root = $actualRoot
    return $policy
}

function Get-PolicyChangedFiles {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    $root = Get-PolicyRepositoryRoot -Policy $Policy
    $statusLines = @(git -C $root status --porcelain=v1 --untracked-files=all)
    if ($LASTEXITCODE -ne 0) { throw "Unable to inspect Git status for $root." }
    return @($statusLines | ForEach-Object {
        if ([string]::IsNullOrWhiteSpace($_) -or $_.Length -lt 4) { return }
        $path = $_.Substring(3).Trim().Trim('"')
        if ($path.Contains(' -> ')) { $path = ($path -split ' -> ')[-1].Trim('"') }
        $path.Replace('\', '/')
    } | Where-Object { -not [string]::IsNullOrWhiteSpace($_) } | Sort-Object -Unique)
}

function Get-PolicyRepositoryName {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    return [string] $Policy.repository.name
}

function Get-PolicyRepositoryRoot {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    return [string] $Policy.repository.root
}

function Get-PolicyVerificationCommand {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    return [string] $Policy.verification.entryPoint
}

function Get-PolicyMode {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    $mode = [string] $Policy.mode
    if (-not [string]::IsNullOrWhiteSpace($env:CODEX_POLICY_MODE_OVERRIDE)) { $mode = $env:CODEX_POLICY_MODE_OVERRIDE }
    if ([string]::IsNullOrWhiteSpace($mode)) { $mode = 'audit' }
    $mode = $mode.ToLowerInvariant()
    if ($mode -notin @('audit', 'enforce')) { throw "Unsupported policy mode '$mode'." }
    return $mode
}

function Get-PolicyToolText {
    param([Parameter(Mandatory = $true)] [object] $EventData)
    if ($null -eq $EventData.tool_input) { return '' }
    if ($null -ne $EventData.tool_input.command) { return [string] $EventData.tool_input.command }
    return ($EventData.tool_input | ConvertTo-Json -Depth 16 -Compress)
}

function Test-PolicyEventInRepository {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [object] $EventData,
        [AllowEmptyString()] [string] $ToolText
    )
    $root = (ConvertTo-PolicyNormalizedText (Get-PolicyRepositoryRoot -Policy $Policy)).TrimEnd('/')
    $cwd = (ConvertTo-PolicyNormalizedText ([string] $EventData.cwd)).TrimEnd('/')
    $cwdIsLocal = $cwd -eq $root -or $cwd.StartsWith("$root/")
    $absolutePaths = [regex]::Matches($ToolText, '(?i)(?<![A-Za-z0-9_])[A-Z]:[\\/][^\s"''`]+')
    $hasLocalAbsolutePath = $false
    foreach ($match in $absolutePaths) {
        $path = (ConvertTo-PolicyNormalizedText ([string] $match.Value)).TrimEnd('/', '.', ',', ';', ':', ')', ']')
        if ($path -eq $root -or $path.StartsWith("$root/")) { $hasLocalAbsolutePath = $true; break }
    }
    if ($absolutePaths.Count -gt 0 -and -not $hasLocalAbsolutePath) { return $false }
    return ($cwdIsLocal -or $hasLocalAbsolutePath)
}

function Test-PolicyPathMatchesPattern {
    param(
        [Parameter(Mandatory = $true)] [string] $Path,
        [Parameter(Mandatory = $true)] [string] $Pattern
    )
    $normalizedPath = (ConvertTo-PolicyNormalizedText $Path).TrimStart('.', '/')
    $normalizedPattern = (ConvertTo-PolicyNormalizedText $Pattern).TrimStart('.', '/')
    return $normalizedPath -like $normalizedPattern
}

function Test-PolicyPathMatchesAnyPattern {
    param(
        [Parameter(Mandatory = $true)] [string] $Path,
        [object[]] $Patterns = @()
    )
    foreach ($pattern in $Patterns) {
        if ([string]::IsNullOrWhiteSpace([string] $pattern)) { continue }
        if (Test-PolicyPathMatchesPattern -Path $Path -Pattern ([string] $pattern)) { return $true }
    }
    return $false
}

function Get-PolicyRiskRank {
    param([Parameter(Mandatory = $true)] [string] $Risk)
    switch ($Risk.ToUpperInvariant()) {
        'L1' { return 1 }
        'L2' { return 2 }
        'L3' { return 3 }
        default { throw "Unsupported risk level '$Risk'." }
    }
}

function Get-PolicyHigherRisk {
    param(
        [Parameter(Mandatory = $true)] [string] $Left,
        [Parameter(Mandatory = $true)] [string] $Right
    )
    if ((Get-PolicyRiskRank $Right) -gt (Get-PolicyRiskRank $Left)) { return $Right.ToUpperInvariant() }
    return $Left.ToUpperInvariant()
}

function Get-PolicyRiskLevelForPaths {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [string[]] $Paths = @()
    )
    $risk = [string] $Policy.riskClassification.default
    foreach ($pathRule in @($Policy.riskClassification.pathRules)) {
        foreach ($path in $Paths) {
            foreach ($pattern in @($pathRule.patterns)) {
                if (Test-PolicyPathMatchesPattern -Path $path -Pattern ([string] $pattern)) {
                    $risk = Get-PolicyHigherRisk -Left $risk -Right ([string] $pathRule.risk)
                }
            }
        }
    }
    return $risk
}

function Test-PolicyRuleMatch {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [object] $Rule,
        [Parameter(Mandatory = $true)] [string] $ToolName,
        [AllowEmptyString()] [string] $ToolText
    )
    if ($null -ne $Rule.appliesToTools -and @($Rule.appliesToTools).Count -gt 0 -and $ToolName -notin @($Rule.appliesToTools)) { return $false }
    $normalized = ConvertTo-PolicyNormalizedText $ToolText
    switch ([string] $Rule.kind) {
        'generatedPathEdit' {
            foreach ($path in @($Policy.generatedPaths)) {
                if ($normalized.Contains((ConvertTo-PolicyNormalizedText ([string] $path)))) { return $true }
            }
            return $false
        }
        'protectedPathEdit' {
            foreach ($item in @($Policy.protectedPaths)) {
                if ($ToolText -match ([string] $item.pattern)) { return $true }
            }
            return $false
        }
        'directGenerator' {
            foreach ($trusted in @($Policy.generators.trusted)) {
                if ($normalized.Contains((ConvertTo-PolicyNormalizedText ([string] $trusted)))) { return $false }
            }
            foreach ($direct in @($Policy.generators.direct)) {
                if ($normalized.Contains((ConvertTo-PolicyNormalizedText ([string] $direct)))) { return $true }
            }
            return $false
        }
        'commandRegex' { return $ToolText -match ([string] $Rule.pattern) }
        default { throw "Unsupported policy rule kind '$($Rule.kind)'." }
    }
}

function Get-PolicyRiskLevelForTool {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [string] $ToolName,
        [AllowEmptyString()] [string] $ToolText
    )
    $risk = [string] $Policy.riskClassification.default
    foreach ($rule in @($Policy.rules)) {
        if (Test-PolicyRuleMatch -Policy $Policy -Rule $rule -ToolName $ToolName -ToolText $ToolText) {
            $risk = Get-PolicyHigherRisk -Left $risk -Right ([string] $rule.risk)
        }
    }
    $normalized = ConvertTo-PolicyNormalizedText $ToolText
    foreach ($pathRule in @($Policy.riskClassification.pathRules)) {
        foreach ($pattern in @($pathRule.patterns)) {
            $literalPrefix = (ConvertTo-PolicyNormalizedText ([string] $pattern)).TrimStart('.', '/')
            $wildcardIndex = $literalPrefix.IndexOfAny([char[]]@('*', '?'))
            if ($wildcardIndex -ge 0) { $literalPrefix = $literalPrefix.Substring(0, $wildcardIndex) }
            if (-not [string]::IsNullOrWhiteSpace($literalPrefix) -and $normalized.Contains($literalPrefix)) {
                $risk = Get-PolicyHigherRisk -Left $risk -Right ([string] $pathRule.risk)
            }
        }
    }
    return $risk
}

function Get-PolicyStateRoot {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    if (-not [string]::IsNullOrWhiteSpace($env:CODEX_POLICY_STATE_ROOT)) { return $env:CODEX_POLICY_STATE_ROOT }
    $safeName = (Get-PolicyRepositoryName -Policy $Policy) -replace '[^A-Za-z0-9_.-]', '_'
    return (Join-Path $env:TEMP "codex-policy-state\$safeName")
}

function Get-PolicySessionStatePath {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [string] $SessionId
    )
    $safeSession = $SessionId -replace '[^A-Za-z0-9_.-]', '_'
    if ([string]::IsNullOrWhiteSpace($safeSession)) { $safeSession = 'unknown-session' }
    return (Join-Path (Get-PolicyStateRoot -Policy $Policy) "$safeSession.json")
}

function Read-PolicySessionState {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [string] $SessionId
    )
    $path = Get-PolicySessionStatePath -Policy $Policy -SessionId $SessionId
    if (-not (Test-Path -LiteralPath $path)) { return $null }
    return (Get-Content -Raw -LiteralPath $path | ConvertFrom-Json)
}

function Write-PolicySessionState {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [string] $SessionId,
        [Parameter(Mandatory = $true)] [object] $State
    )
    $root = Get-PolicyStateRoot -Policy $Policy
    New-Item -ItemType Directory -Force -Path $root | Out-Null
    $path = Get-PolicySessionStatePath -Policy $Policy -SessionId $SessionId
    $State | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $path -Encoding UTF8
}

function Write-PolicyAuditRecord {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [object] $Record
    )
    $safeName = (Get-PolicyRepositoryName -Policy $Policy) -replace '[^A-Za-z0-9_.-]', '_'
    $auditRoot = if (-not [string]::IsNullOrWhiteSpace($env:CODEX_POLICY_AUDIT_ROOT)) {
        Join-Path $env:CODEX_POLICY_AUDIT_ROOT $safeName
    }
    else {
        Join-Path $env:TEMP "codex-policy-audit\$safeName"
    }
    New-Item -ItemType Directory -Force -Path $auditRoot | Out-Null
    $auditPath = Join-Path $auditRoot ("policy-{0}.jsonl" -f (Get-Date -Format 'yyyyMMdd'))
    ($Record | ConvertTo-Json -Depth 12 -Compress) | Add-Content -LiteralPath $auditPath -Encoding UTF8
}

function Test-PolicyToolSucceeded {
    param([object] $ToolResponse)
    if ($null -eq $ToolResponse) { return $true }
    foreach ($name in @('exitCode', 'exit_code')) {
        if ($null -ne $ToolResponse.PSObject.Properties[$name]) { return ([int] $ToolResponse.$name -eq 0) }
    }
    if ($null -ne $ToolResponse.PSObject.Properties['isError']) { return (-not [bool] $ToolResponse.isError) }
    $text = $ToolResponse | ConvertTo-Json -Depth 16 -Compress
    if ($text -match '(?i)exit\s*code\s*[:=]\s*[1-9]\d*' -or $text -match '(?i)script\s+(?:failed|error)' -or $text -match '"isError"\s*:\s*true') { return $false }
    return $true
}

function Test-PolicyMutatingTool {
    param(
        [Parameter(Mandatory = $true)] [string] $ToolName,
        [AllowEmptyString()] [string] $ToolText
    )
    if ($ToolName -in @('apply_patch', 'Edit', 'Write')) { return $true }
    return $ToolText -match '(?i)\b(Set-Content|Add-Content|Out-File|Copy-Item|Move-Item|Remove-Item|New-Item|Rename-Item|git\s+apply|dotnet\s+format|npm\s+version)\b|(?:--write\b)|(?:regenerate|generate|sync)-[^\s"'']+\.ps1\b'
}

function Test-PolicyVerificationCommand {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [AllowEmptyString()] [string] $ToolText
    )
    $expected = ConvertTo-PolicyNormalizedText (Get-PolicyVerificationCommand -Policy $Policy)
    if ([string]::IsNullOrWhiteSpace($expected)) { return $false }
    return (ConvertTo-PolicyNormalizedText $ToolText).Contains($expected)
}
