$ErrorActionPreference = 'Stop'

function Write-PolicyHookJson {
    param([Parameter(Mandatory = $true)] [object] $Value)
    $Value | ConvertTo-Json -Depth 16 -Compress
}

function Get-PolicySha256 {
    param([Parameter(Mandatory = $true)] [string] $Value)
    $sha = [System.Security.Cryptography.SHA256]::Create()
    try {
        $bytes = [System.Text.Encoding]::UTF8.GetBytes($Value)
        $hash = $sha.ComputeHash($bytes)
        return ([System.BitConverter]::ToString($hash)).Replace('-', '').ToLowerInvariant()
    }
    finally {
        $sha.Dispose()
    }
}

function Get-PolicyData {
    param([Parameter(Mandatory = $true)] [string] $PolicyPath)
    $resolved = (Resolve-Path -LiteralPath $PolicyPath).Path
    $policy = Get-Content -Raw -LiteralPath $resolved | ConvertFrom-Json
    if ([int] $policy.version -ne 1) {
        throw "Unsupported policy version in $resolved."
    }
    if ([string]::IsNullOrWhiteSpace([string] $policy.repositoryName) -or
        [string]::IsNullOrWhiteSpace([string] $policy.repositoryRoot)) {
        throw "Policy must define repositoryName and repositoryRoot."
    }
    return $policy
}

function Get-PolicyMode {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    $mode = [string] $Policy.mode
    if (-not [string]::IsNullOrWhiteSpace($env:CODEX_POLICY_MODE_OVERRIDE)) {
        $mode = $env:CODEX_POLICY_MODE_OVERRIDE
    }
    if ([string]::IsNullOrWhiteSpace($mode)) { $mode = 'audit' }
    return $mode.ToLowerInvariant()
}

function Test-PolicyEventInRepository {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [Parameter(Mandatory = $true)] [object] $EventData,
        [AllowEmptyString()] [string] $ToolText
    )
    $root = (ConvertTo-PolicyNormalizedText ([string] $Policy.repositoryRoot)).TrimEnd('/')
    $cwd = (ConvertTo-PolicyNormalizedText ([string] $EventData.cwd)).TrimEnd('/')
    $normalized = ConvertTo-PolicyNormalizedText $ToolText
    $cwdIsLocal = $cwd -eq $root -or $cwd.StartsWith("$root/")
    $mentionsRoot = $normalized.Contains("$root/") -or $normalized.Contains($root)

    # An explicit absolute path outside this repository makes the event out of
    # scope. Project policy must never become a machine-wide command policy.
    $absolutePaths = [regex]::Matches($ToolText, '(?i)(?<![A-Za-z0-9_])[A-Z]:[\\/][^\s"''`]+')
    foreach ($match in $absolutePaths) {
        $path = (ConvertTo-PolicyNormalizedText ([string] $match.Value)).TrimEnd('/', '.', ',', ';', ':', ')', ']')
        if ($path -ne $root -and -not $path.StartsWith("$root/")) {
            return $false
        }
    }

    return ($cwdIsLocal -or $mentionsRoot)
}

function Get-PolicyToolText {
    param([Parameter(Mandatory = $true)] [object] $EventData)
    if ($null -eq $EventData.tool_input) { return '' }
    if ($null -ne $EventData.tool_input.command) {
        return [string] $EventData.tool_input.command
    }
    return ($EventData.tool_input | ConvertTo-Json -Depth 16 -Compress)
}

function ConvertTo-PolicyNormalizedText {
    param([AllowEmptyString()] [string] $Value)
    if ($null -eq $Value) { return '' }
    return $Value.Replace('\', '/').ToLowerInvariant()
}

function Get-PolicyStateRoot {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    if (-not [string]::IsNullOrWhiteSpace($env:CODEX_POLICY_STATE_ROOT)) {
        return $env:CODEX_POLICY_STATE_ROOT
    }
    $safeName = ([string] $Policy.repositoryName) -replace '[^A-Za-z0-9_.-]', '_'
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
    $safeName = ([string] $Policy.repositoryName) -replace '[^A-Za-z0-9_.-]', '_'
    $auditRoot = Join-Path $env:TEMP "codex-policy-audit\$safeName"
    New-Item -ItemType Directory -Force -Path $auditRoot | Out-Null
    $auditPath = Join-Path $auditRoot ("policy-{0}.jsonl" -f (Get-Date -Format 'yyyyMMdd'))
    ($Record | ConvertTo-Json -Depth 12 -Compress) | Add-Content -LiteralPath $auditPath -Encoding UTF8
}

function Test-PolicyToolSucceeded {
    param([object] $ToolResponse)
    if ($null -eq $ToolResponse) { return $true }
    foreach ($name in @('exitCode', 'exit_code')) {
        if ($null -ne $ToolResponse.PSObject.Properties[$name]) {
            return ([int] $ToolResponse.$name -eq 0)
        }
    }
    if ($null -ne $ToolResponse.PSObject.Properties['isError']) {
        return (-not [bool] $ToolResponse.isError)
    }
    $text = $ToolResponse | ConvertTo-Json -Depth 16 -Compress
    if ($text -match '(?i)exit\s*code\s*[:=]\s*[1-9]\d*' -or
        $text -match '(?i)script\s+(?:failed|error)' -or
        $text -match '"isError"\s*:\s*true') {
        return $false
    }
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
    $expected = ConvertTo-PolicyNormalizedText ([string] $Policy.verificationCommand)
    if ([string]::IsNullOrWhiteSpace($expected)) { return $false }
    return (ConvertTo-PolicyNormalizedText $ToolText).Contains($expected)
}
