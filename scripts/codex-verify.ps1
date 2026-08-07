param(
    [switch] $Full,
    [switch] $Json,
    [switch] $PolicyOnly,
    [ValidateSet('Auto', 'L1', 'L2', 'L3')]
    [string] $Risk = 'Auto',
    [string] $ReceiptPathOverride = ''
)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
. (Join-Path $repoRoot '.codex\hooks\PolicyCommon.ps1')
. (Join-Path $repoRoot '.codex\hooks\VerificationCommon.ps1')
$policy = Get-PolicyData -PolicyPath $policyPath
Reset-CodexVerificationResults
$quiet = [bool] $Json
$changedFiles = Get-PolicyChangedFiles -Policy $policy

$generatedFilesChanged = @($changedFiles | Where-Object {
    Test-PolicyPathMatchesAnyPattern -Path $_ -Patterns @($policy.generatedPaths)
})
$sourcePatterns = @($policy.sourcesOfTruth | ForEach-Object { @($_.patterns) })
$sourceFilesChanged = @($changedFiles | Where-Object {
    Test-PolicyPathMatchesAnyPattern -Path $_ -Patterns $sourcePatterns
})

if ($generatedFilesChanged.Count -gt 0 -and $sourceFilesChanged.Count -eq 0) {
    Add-CodexVerificationResult 'generated-file-consistency' $false 1 ("Generated files changed without an authoritative source change:`n" + ($generatedFilesChanged -join "`n"))
}
else {
    Add-CodexVerificationResult 'generated-file-consistency' $true 0 'Generated changes are absent or accompanied by an authoritative source change.'
}

$riskLevel = if ($Risk -eq 'Auto') { Get-PolicyRiskLevelForPaths -Policy $policy -Paths $changedFiles } else { $Risk }
$riskRank = Get-PolicyRiskRank $riskLevel
$dependencyImpacts = @($policy.dependencies | ForEach-Object {
    $dependency = $_
    $impacted = $false
    foreach ($changedFile in $changedFiles) {
        if (Test-PolicyPathMatchesAnyPattern -Path $changedFile -Patterns @($dependency.impactPatterns)) { $impacted = $true }
    }
    [ordered]@{
        repository = [string] $dependency.repository
        relationship = [string] $dependency.relationship
        impacted = $impacted
        compatibilityVerified = $false
        defaultAccess = [string] $dependency.defaultAccess
    }
})
foreach ($check in @($policy.verification.checks)) {
    $name = [string] $check.name
    if ($PolicyOnly -and [string] $check.category -ne 'policy') {
        Add-CodexSkippedCheck -Name $name -Reason 'PolicyOnly was requested.'
        continue
    }

    $pathMatched = $false
    foreach ($changedFile in $changedFiles) {
        foreach ($pattern in @($check.whenPaths)) {
            if ([string]::IsNullOrWhiteSpace([string] $pattern)) { continue }
            if (Test-PolicyPathMatchesPattern -Path $changedFile -Pattern ([string] $pattern)) { $pathMatched = $true }
        }
    }
    $riskMatched = $false
    if ([bool] $check.runAtOrAbove -and -not [string]::IsNullOrWhiteSpace([string] $check.minimumRisk)) {
        $riskMatched = $riskRank -ge (Get-PolicyRiskRank ([string] $check.minimumRisk))
    }
    $shouldRun = $Full -or [bool] $check.always -or $pathMatched -or $riskMatched
    if (-not $shouldRun) {
        Add-CodexSkippedCheck -Name $name -Reason "Not selected for risk $riskLevel or changed paths."
        continue
    }

    $preconditionFailure = $null
    foreach ($precondition in @($check.preconditions)) {
        if ($null -eq $precondition) { continue }
        if ([string] $precondition.kind -eq 'anyPathExists') {
            $found = $false
            foreach ($candidatePath in @($precondition.paths)) {
                if (Test-Path -LiteralPath (Join-Path $repoRoot ([string] $candidatePath))) { $found = $true }
            }
            if (-not $found) { $preconditionFailure = [string] $precondition.message }
        }
    }
    if (-not [string]::IsNullOrWhiteSpace($preconditionFailure)) {
        Add-CodexVerificationResult -Name $name -Success:$false -ExitCode 1 -Output "Tooling prerequisite not found. $preconditionFailure" -Command (([string] $check.executable) + ' ' + (@($check.arguments) -join ' '))
        continue
    }

    $workingDirectory = (Resolve-Path -LiteralPath (Join-Path $repoRoot ([string] $check.workingDirectory))).Path
    Invoke-CodexVerificationCheck -Name $name -WorkingDirectory $workingDirectory -Executable ([string] $check.executable) -Arguments @($check.arguments | ForEach-Object { [string] $_ }) -Quiet:$quiet
    if ([string] $check.category -eq 'frontend') {
        $lastResult = $script:CodexVerificationResults[-1]
        if (-not [bool] $lastResult.success) { $lastResult.failureKind = 'compile-or-typecheck' }
    }
}

$receiptRelativePath = if ([string]::IsNullOrWhiteSpace($ReceiptPathOverride)) { [string] $policy.verification.receiptPath } else { $ReceiptPathOverride }
Assert-PolicyRelativePath -Path $receiptRelativePath -Field 'ReceiptPathOverride'
$receiptPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $receiptRelativePath))
$normalizedRepoRoot = $repoRoot.TrimEnd('\') + '\'
if (-not $receiptPath.StartsWith($normalizedRepoRoot, [System.StringComparison]::OrdinalIgnoreCase)) { throw 'Verification receipt must remain inside the JCS repository.' }
Write-CodexVerificationReport -Repository $repoRoot -RepositoryName (Get-PolicyRepositoryName -Policy $policy) -RiskLevel $riskLevel -ChangedFiles $changedFiles -GeneratedFilesChanged $generatedFilesChanged -SourceFilesChanged $sourceFilesChanged -DependencyImpacts $dependencyImpacts -ReceiptPath $receiptPath -Full:$Full -PolicyOnly:$PolicyOnly -Json:$Json
if (-not $script:CodexVerificationSuccess) { exit 1 }
exit 0
