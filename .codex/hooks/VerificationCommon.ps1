$script:CodexVerificationResults = @()
$script:CodexVerificationSkipped = @()
$script:CodexVerificationSuccess = $false
$script:CodexVerificationReport = $null

function Reset-CodexVerificationResults {
    $script:CodexVerificationResults = @()
    $script:CodexVerificationSkipped = @()
    $script:CodexVerificationSuccess = $false
    $script:CodexVerificationReport = $null
}

function Add-CodexVerificationResult {
    param([string] $Name, [bool] $Success, [int] $ExitCode, [string] $Output, [string] $Command = '', [string[]] $EvidenceLevels = @())
    $text = if ($null -eq $Output) { '' } else { $Output.Trim() }
    if ($text.Length -gt 12000) { $text = $text.Substring($text.Length - 12000) }
    $failureKind = $null
    if (-not $Success) {
        $failureKind = if ($text -match '(?i)\berror\s+(?:FS|CS|TS)\d+|typecheck|compilation failed|build failed') {
            'compile-or-typecheck'
        }
        elseif ($text -match '(?i)tooling prerequisite not found|script not found|command not found|not recognized as (?:a |the )?(?:name of a )?(?:cmdlet|command)|no such file or directory') {
            'tooling-prerequisite'
        }
        elseif ($text -match '(?i)\b(assert|expected|actual|test(?:s)? failed|failure)\b') { 'test-failure' }
        elseif ($text -match '(?i)\b(timeout|timed out)\b') { 'timeout' }
        elseif ($text -match '(?i)\b(unauthorized|forbidden|permission denied|access denied)\b') { 'authorization' }
        else { 'unknown-failure' }
    }
    $script:CodexVerificationResults += [pscustomobject]@{
        name = $Name
        success = $Success
        exitCode = $ExitCode
        output = $text
        command = $Command
        failureKind = $failureKind
        evidenceLevels = @($EvidenceLevels)
    }
}

function Add-CodexSkippedCheck {
    param([string] $Name, [string] $Reason)
    $script:CodexVerificationSkipped += [pscustomobject]@{ name = $Name; reason = $Reason }
}

function Invoke-CodexVerificationCheck {
    param(
        [Parameter(Mandatory = $true)] [string] $Name,
        [Parameter(Mandatory = $true)] [string] $WorkingDirectory,
        [Parameter(Mandatory = $true)] [string] $Executable,
        [string[]] $Arguments = @(),
        [string[]] $EvidenceLevels = @(),
        [switch] $Quiet
    )
    $previousErrorActionPreference = $ErrorActionPreference
    Push-Location $WorkingDirectory
    try {
        $ErrorActionPreference = 'Continue'
        $output = (& $Executable @Arguments 2>&1 | Out-String)
        $exitCode = $LASTEXITCODE
        if ($null -eq $exitCode) { $exitCode = 0 }
    }
    catch {
        $output = $_ | Out-String
        $exitCode = 1
    }
    finally {
        $ErrorActionPreference = $previousErrorActionPreference
        Pop-Location
    }
    $commandText = ($Executable + ' ' + ($Arguments -join ' ')).Trim()
    Add-CodexVerificationResult -Name $Name -Success:($exitCode -eq 0) -ExitCode $exitCode -Output $output -Command $commandText -EvidenceLevels $EvidenceLevels
    if (-not $Quiet) {
        Write-Output "[$Name] exit=$exitCode"
        if (-not [string]::IsNullOrWhiteSpace($output)) { Write-Output $output.TrimEnd() }
    }
}

function Write-CodexVerificationReport {
    param(
        [Parameter(Mandatory = $true)] [string] $Repository,
        [Parameter(Mandatory = $true)] [string] $RepositoryName,
        [Parameter(Mandatory = $true)] [string] $RiskLevel,
        [string[]] $ChangedFiles = @(),
        [string[]] $GeneratedFilesChanged = @(),
        [string[]] $SourceFilesChanged = @(),
        [object[]] $DependencyImpacts = @(),
        [object] $Coverage = $null,
        [object] $TaskProfile = $null,
        [AllowEmptyString()] [string] $SessionId = '',
        [Parameter(Mandatory = $true)] [string] $ReceiptPath,
        [bool] $Full = $false,
        [bool] $PolicyOnly = $false,
        [switch] $Json
    )
    $failed = @($script:CodexVerificationResults | Where-Object { -not $_.success })
    foreach ($failedCheck in $failed) {
        if ([string] $failedCheck.name -match '(?i)typecheck$') { $failedCheck.failureKind = 'compile-or-typecheck' }
    }
    $checks = @($script:CodexVerificationResults | ForEach-Object {
        [ordered]@{ name = $_.name; command = $_.command; passed = [bool] $_.success; exitCode = [int] $_.exitCode; failureKind = $_.failureKind; evidenceLevels = @($_.evidenceLevels) }
    })
    $risks = @()
    if ($GeneratedFilesChanged.Count -gt 0 -and $SourceFilesChanged.Count -eq 0) {
        $risks += 'Generated files changed without an authoritative source change.'
    }
    if ($RiskLevel -eq 'L3') {
        $risks += 'Any associated L3 action requires separate explicit user authorization; the verifier does not grant it.'
    }
    foreach ($impact in $DependencyImpacts) {
        if ([bool] $impact.impacted -and -not [bool] $impact.compatibilityVerified) {
            $risks += "Dependency compatibility remains unverified: $($impact.repository)."
        }
    }
    foreach ($failedCheck in $failed) {
        $risks += "Verification failed: $($failedCheck.name) [$($failedCheck.failureKind)]."
    }
    $confidence = if ($null -eq $Coverage -or [string]::IsNullOrWhiteSpace([string] $Coverage.confidence)) { 'unknown' } else { [string] $Coverage.confidence }
    if ($confidence -in @('none', 'partial', 'unknown')) {
        $risks += "Verification coverage is $confidence; passing selected checks is not complete evidence for every changed behavior."
    }
    $recommendedActions = if ($null -eq $Coverage) { @('Report that verification coverage could not be determined.') } else { @($Coverage.recommendedActions) }
    $report = [ordered]@{
        schemaVersion = 3
        timestamp = (Get-Date).ToUniversalTime().ToString('o')
        sessionId = $SessionId
        repository = $RepositoryName
        repositoryRoot = $Repository
        riskLevel = $RiskLevel
        full = $Full
        policyOnly = $PolicyOnly
        success = $failed.Count -eq 0
        verificationConfidence = $confidence
        taskProfile = $TaskProfile
        coverage = $Coverage
        changedFiles = @($ChangedFiles)
        generatedFilesChanged = @($GeneratedFilesChanged)
        authoritativeSourcesChanged = @($SourceFilesChanged)
        dependencyImpacts = @($DependencyImpacts)
        userChangesPreserved = 'not-evaluated-by-verifier'
        checks = $checks
        skippedChecks = @($script:CodexVerificationSkipped)
        externalSystemsModified = $false
        risks = $risks
        recommendedActions = @($recommendedActions)
    }
    $receiptDirectory = Split-Path -Parent $ReceiptPath
    New-Item -ItemType Directory -Force -Path $receiptDirectory | Out-Null
    $report | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $ReceiptPath -Encoding UTF8
    if ($Json) {
        $report | ConvertTo-Json -Depth 12
    }
    else {
        Write-Output ("Verification result: {0}; risk={1}; confidence={2}; checks={3}; skipped={4}; changed-files={5}" -f $(if ($report.success) { 'PASS' } else { 'FAIL' }), $RiskLevel, $confidence, $checks.Count, $script:CodexVerificationSkipped.Count, $ChangedFiles.Count)
        Write-Output "Receipt: $ReceiptPath"
    }
    $script:CodexVerificationSuccess = [bool] $report.success
    $script:CodexVerificationReport = $report
}
