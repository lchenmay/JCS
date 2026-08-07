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
    param([string] $Name, [bool] $Success, [int] $ExitCode, [string] $Output, [string] $Command = '')
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
        else { 'command-failure' }
    }
    $script:CodexVerificationResults += [pscustomobject]@{
        name = $Name
        success = $Success
        exitCode = $ExitCode
        output = $text
        command = $Command
        failureKind = $failureKind
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
    Add-CodexVerificationResult -Name $Name -Success:($exitCode -eq 0) -ExitCode $exitCode -Output $output -Command $commandText
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
        [ordered]@{ name = $_.name; command = $_.command; passed = [bool] $_.success; exitCode = [int] $_.exitCode; failureKind = $_.failureKind }
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
    $report = [ordered]@{
        schemaVersion = 1
        timestamp = (Get-Date).ToUniversalTime().ToString('o')
        repository = $RepositoryName
        repositoryRoot = $Repository
        riskLevel = $RiskLevel
        full = $Full
        policyOnly = $PolicyOnly
        success = $failed.Count -eq 0
        changedFiles = @($ChangedFiles)
        generatedFilesChanged = @($GeneratedFilesChanged)
        authoritativeSourcesChanged = @($SourceFilesChanged)
        dependencyImpacts = @($DependencyImpacts)
        userChangesPreserved = 'not-evaluated-by-verifier'
        checks = $checks
        skippedChecks = @($script:CodexVerificationSkipped)
        externalSystemsModified = $false
        risks = $risks
    }
    $receiptDirectory = Split-Path -Parent $ReceiptPath
    New-Item -ItemType Directory -Force -Path $receiptDirectory | Out-Null
    $report | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $ReceiptPath -Encoding UTF8
    if ($Json) {
        $report | ConvertTo-Json -Depth 12
    }
    else {
        Write-Output ("Verification result: {0}; risk={1}; checks={2}; skipped={3}; changed-files={4}" -f $(if ($report.success) { 'PASS' } else { 'FAIL' }), $RiskLevel, $checks.Count, $script:CodexVerificationSkipped.Count, $ChangedFiles.Count)
        Write-Output "Receipt: $ReceiptPath"
    }
    $script:CodexVerificationSuccess = [bool] $report.success
    $script:CodexVerificationReport = $report
}
