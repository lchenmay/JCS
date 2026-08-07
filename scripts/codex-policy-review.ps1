param([switch] $Json)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
. (Join-Path $repoRoot '.codex\hooks\PolicyCommon.ps1')
$policy = Get-PolicyData -PolicyPath $policyPath
$changedFiles = Get-PolicyChangedFiles -Policy $policy
$risk = Get-PolicyRiskLevelForPaths -Policy $policy -Paths $changedFiles
$riskRank = Get-PolicyRiskRank $risk

$plannedChecks = @()
foreach ($check in @($policy.verification.checks)) {
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
    if ([bool] $check.always -or $pathMatched -or $riskMatched) { $plannedChecks += [string] $check.name }
}

$review = [ordered]@{
    repository = Get-PolicyRepositoryName -Policy $policy
    repositoryRoot = Get-PolicyRepositoryRoot -Policy $policy
    policyMode = Get-PolicyMode -Policy $policy
    writeScope = [string] $policy.repository.writeScope
    outsideRepository = [string] $policy.repository.outsideRepository
    inferredRisk = $risk
    changedFiles = $changedFiles
    plannedChecks = $plannedChecks
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
    Write-Output "Planned checks: $($review.plannedChecks -join ', ')"
    Write-Output 'Preview only: no verification commands or external actions were executed.'
}
