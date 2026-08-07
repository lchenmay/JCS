$script:CodexVerificationResults = @()
$script:CodexVerificationSuccess = $false

function Reset-CodexVerificationResults {
    $script:CodexVerificationResults = @()
}

function Add-CodexVerificationResult {
    param([string] $Name, [bool] $Success, [int] $ExitCode, [string] $Output)
    $text = if ($null -eq $Output) { '' } else { $Output.Trim() }
    if ($text.Length -gt 12000) { $text = $text.Substring($text.Length - 12000) }
    $script:CodexVerificationResults += [pscustomobject]@{
        name = $Name
        success = $Success
        exitCode = $ExitCode
        output = $text
    }
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
        # Native tools commonly use stderr for warnings/progress. Judge them by
        # their process exit code instead of turning stderr into a PowerShell
        # terminating error.
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
    Add-CodexVerificationResult -Name $Name -Success:($exitCode -eq 0) -ExitCode $exitCode -Output $output
    if (-not $Quiet) {
        Write-Output "[$Name] exit=$exitCode"
        if (-not [string]::IsNullOrWhiteSpace($output)) { Write-Output $output.TrimEnd() }
    }
}

function Write-CodexVerificationReport {
    param(
        [Parameter(Mandatory = $true)] [string] $Repository,
        [Parameter(Mandatory = $true)] [int] $ChangedFileCount,
        [bool] $Full = $false,
        [bool] $PolicyOnly = $false,
        [switch] $Json
    )
    $failed = @($script:CodexVerificationResults | Where-Object { -not $_.success })
    $report = [ordered]@{
        repository = $Repository
        full = $Full
        policyOnly = $PolicyOnly
        success = $failed.Count -eq 0
        changedFileCount = $ChangedFileCount
        checks = $script:CodexVerificationResults
    }
    if ($Json) {
        $report | ConvertTo-Json -Depth 10
    }
    else {
        Write-Output ("Verification result: {0}; checks={1}; changed-status-lines={2}" -f $(if ($report.success) { 'PASS' } else { 'FAIL' }), $script:CodexVerificationResults.Count, $ChangedFileCount)
    }
    $script:CodexVerificationSuccess = [bool] $report.success
}
