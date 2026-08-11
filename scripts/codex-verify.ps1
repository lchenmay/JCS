param(
    [switch] $Full,
    [switch] $Json,
    [switch] $PolicyOnly,
    [ValidateSet('Auto', 'L1', 'L2', 'L3')]
    [string] $Risk = 'Auto',
    [string[]] $TaskType = @(),
    [string[]] $FocusedCheck = @(),
    [string] $SessionId = '',
    [string] $ReceiptPathOverride = ''
)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
. (Join-Path $repoRoot '.codex\hooks\PolicyCommon.ps1')
. (Join-Path $repoRoot '.codex\hooks\VerificationCommon.ps1')
. (Join-Path $repoRoot '.codex\hooks\VerificationSelection.ps1')
. (Join-Path $repoRoot '.codex\hooks\TaskRouting.ps1')
$policy = Get-PolicyData -PolicyPath $policyPath
Reset-CodexVerificationResults
$quiet = [bool] $Json
$changedFiles = Get-CodexVerificationChangedFiles -Policy $policy

$generatedFilesChanged = @($changedFiles | Where-Object {
    Test-PolicyPathMatchesAnyPattern -Path $_ -Patterns @($policy.generatedPaths)
})
$sourcePatterns = @($policy.sourcesOfTruth | ForEach-Object { @($_.patterns) })
$sourceFilesChanged = @($changedFiles | Where-Object {
    Test-PolicyPathMatchesAnyPattern -Path $_ -Patterns $sourcePatterns
})

if ($generatedFilesChanged.Count -gt 0 -and $sourceFilesChanged.Count -eq 0) {
    Add-CodexVerificationResult 'generated-file-consistency' $false 1 ("Generated files changed without an authoritative source change:`n" + ($generatedFilesChanged -join "`n")) -EvidenceLevels @('generation')
}
else {
    Add-CodexVerificationResult 'generated-file-consistency' $true 0 'Generated changes are absent or accompanied by an authoritative source change.' -EvidenceLevels @('generation')
}

$sessionState = if ([string]::IsNullOrWhiteSpace($SessionId)) { $null } else { Read-PolicySessionState -Policy $policy -SessionId $SessionId }
$effectiveTaskTypes = if ($TaskType.Count -gt 0) { @($TaskType) } elseif ($null -ne $sessionState) { @($sessionState.taskTypes | ForEach-Object { [string] $_ }) } else { @() }
$taskProfile = Get-CodexTaskProfile -Policy $policy -ChangedFiles $changedFiles -ExplicitTaskTypes $effectiveTaskTypes
$pathRisk = Get-PolicyRiskLevelForPaths -Policy $policy -Paths $changedFiles
$autoRisk = Get-PolicyHigherRisk -Left $pathRisk -Right ([string] $taskProfile.inferredRisk)
$riskLevel = if ($Risk -eq 'Auto') { $autoRisk } else { $Risk }
$plan = @(Get-CodexVerificationPlan -Policy $policy -ChangedFiles $changedFiles -RiskLevel $riskLevel -TaskTypes @($taskProfile.taskTypes) -CheckNames $FocusedCheck -Full:$Full -PolicyOnly:$PolicyOnly)
if ($FocusedCheck.Count -gt 0) {
    $knownChecks = @($policy.verification.checks | ForEach-Object { ([string] $_.name).ToLowerInvariant() })
    $unknownChecks = @($FocusedCheck | Where-Object { ([string] $_).ToLowerInvariant() -notin $knownChecks })
    if ($unknownChecks.Count -gt 0) { throw "Unknown focused verification check(s): $($unknownChecks -join ', ')." }
}
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
foreach ($planItem in $plan) {
    $checkDefinition = $planItem.check
    $name = [string] $planItem.name
    if (-not [bool] $planItem.selected) {
        $skipReason = if (@($planItem.reasons).Count -gt 0) { "Selection reasons did not authorize execution: $(@($planItem.reasons) -join ',')." } else { "No changed-path or risk trigger matched at risk $riskLevel." }
        Add-CodexSkippedCheck -Name $name -Reason $skipReason
        continue
    }

    $preconditionFailure = $null
    foreach ($precondition in @($checkDefinition.preconditions)) {
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
        Add-CodexVerificationResult -Name $name -Success:$false -ExitCode 1 -Output "Tooling prerequisite not found. $preconditionFailure" -Command (([string] $checkDefinition.executable) + ' ' + (@($checkDefinition.arguments) -join ' ')) -EvidenceLevels @($checkDefinition.evidenceLevels | ForEach-Object { [string] $_ })
        continue
    }

    $workingDirectory = (Resolve-Path -LiteralPath (Join-Path $repoRoot ([string] $checkDefinition.workingDirectory))).Path
    Invoke-CodexVerificationCheck -Name $name -WorkingDirectory $workingDirectory -Executable ([string] $checkDefinition.executable) -Arguments @($checkDefinition.arguments | ForEach-Object { [string] $_ }) -EvidenceLevels @($checkDefinition.evidenceLevels | ForEach-Object { [string] $_ }) -Quiet:$quiet
    if ([string] $checkDefinition.category -eq 'frontend') {
        $lastResult = $script:CodexVerificationResults[-1]
        if (-not [bool] $lastResult.success) { $lastResult.failureKind = 'compile-or-typecheck' }
    }
}

$coverage = Get-CodexVerificationCoverage -Policy $policy -ChangedFiles $changedFiles -Plan $plan -TaskProfile $taskProfile -Results @($script:CodexVerificationResults)
$receiptRelativePath = if (-not [string]::IsNullOrWhiteSpace($ReceiptPathOverride)) {
    $ReceiptPathOverride
}
else {
    Get-PolicyVerificationReceiptRelativePath -Policy $policy -SessionId $SessionId
}
Assert-PolicyRelativePath -Path $receiptRelativePath -Field 'ReceiptPathOverride'
$receiptPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $receiptRelativePath))
$normalizedRepoRoot = $repoRoot.TrimEnd('\') + '\'
if (-not $receiptPath.StartsWith($normalizedRepoRoot, [System.StringComparison]::OrdinalIgnoreCase)) { throw 'Verification receipt must remain inside the JCS repository.' }
Write-CodexVerificationReport -Repository $repoRoot -RepositoryName (Get-PolicyRepositoryName -Policy $policy) -RiskLevel $riskLevel -ChangedFiles $changedFiles -GeneratedFilesChanged $generatedFilesChanged -SourceFilesChanged $sourceFilesChanged -DependencyImpacts $dependencyImpacts -Coverage $coverage -TaskProfile $taskProfile -SessionId $SessionId -ReceiptPath $receiptPath -Full:$Full -PolicyOnly:$PolicyOnly -Json:$Json
if (-not $script:CodexVerificationSuccess) { exit 1 }
exit 0
