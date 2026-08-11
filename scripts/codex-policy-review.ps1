param([switch] $Json, [string] $Prompt = '', [string[]] $TaskType = @())

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
. (Join-Path $repoRoot '.codex\hooks\PolicyCommon.ps1')
. (Join-Path $repoRoot '.codex\hooks\VerificationSelection.ps1')
. (Join-Path $repoRoot '.codex\hooks\TaskRouting.ps1')
$policy = Get-PolicyData -PolicyPath $policyPath
$changedFiles = Get-CodexVerificationChangedFiles -Policy $policy
$taskProfile = Get-CodexTaskProfile -Policy $policy -Prompt $Prompt -ChangedFiles $changedFiles -ExplicitTaskTypes $TaskType
$pathRisk = Get-PolicyRiskLevelForPaths -Policy $policy -Paths $changedFiles
$risk = Get-PolicyHigherRisk -Left $pathRisk -Right ([string] $taskProfile.inferredRisk)
$plan = @(Get-CodexVerificationPlan -Policy $policy -ChangedFiles $changedFiles -RiskLevel $risk -TaskTypes @($taskProfile.taskTypes))
$plannedChecks = @($plan | Where-Object { $_.selected } | ForEach-Object { [string] $_.name })
$coverage = Get-CodexVerificationCoverage -Policy $policy -ChangedFiles $changedFiles -Plan $plan -TaskProfile $taskProfile

$review = [ordered]@{
    repository = Get-PolicyRepositoryName -Policy $policy
    repositoryRoot = Get-PolicyRepositoryRoot -Policy $policy
    policyMode = Get-PolicyMode -Policy $policy
    writeScope = [string] $policy.repository.writeScope
    outsideRepository = [string] $policy.repository.outsideRepository
    inferredRisk = $risk
    taskProfile = $taskProfile
    changedFiles = $changedFiles
    plannedChecks = $plannedChecks
    verificationConfidence = [string] $coverage.confidence
    uncoveredPaths = @($coverage.uncoveredPaths)
    recommendedActions = @($coverage.recommendedActions)
    dependencies = @($policy.dependencies | ForEach-Object {
        [ordered]@{
            repository = [string] $_.repository
            relationship = [string] $_.relationship
            defaultAccess = [string] $_.defaultAccess
            automaticVerification = [bool] $_.automaticVerification
        }
    })
    executesChecks = $false
    modifiesExternalSystems = $false
}
if ($Json) { $review | ConvertTo-Json -Depth 10 }
else {
    Write-Output "Repository: $($review.repository) ($($review.repositoryRoot))"
    Write-Output "Mode/scope: $($review.policyMode); $($review.writeScope); outside=$($review.outsideRepository)"
    Write-Output "Inferred risk: $($review.inferredRisk)"
    Write-Output "Task profile: status=$($taskProfile.status); types=$(@($taskProfile.taskTypes) -join ','); evidence=$(@($taskProfile.requiredEvidence) -join ',')"
    Write-Output "Planned checks: $($review.plannedChecks -join ', ')"
    Write-Output "Verification confidence: $($review.verificationConfidence)"
    if ($review.uncoveredPaths.Count -gt 0) { Write-Output "Uncovered paths: $($review.uncoveredPaths -join ', ')" }
    Write-Output 'Preview only: no verification commands or external actions were executed.'
}
