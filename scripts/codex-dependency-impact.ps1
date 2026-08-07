param([switch] $Json)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
. (Join-Path $repoRoot '.codex\hooks\PolicyCommon.ps1')
$policy = Get-PolicyData -PolicyPath $policyPath
$changedFiles = Get-PolicyChangedFiles -Policy $policy
$results = @()
$success = $true

foreach ($dependency in @($policy.dependencies)) {
    $dependencyPath = [string] $dependency.path
    $exists = Test-Path -LiteralPath $dependencyPath -PathType Container
    $isGitRepository = $false
    $head = $null
    $dirtyStatusCount = $null
    if ($exists) {
        $gitRoot = (& git -C $dependencyPath rev-parse --show-toplevel 2>$null | Out-String).Trim()
        $isGitRepository = $LASTEXITCODE -eq 0
        if ($isGitRepository) {
            $head = (& git -C $dependencyPath rev-parse HEAD 2>$null | Out-String).Trim()
            $dirtyStatusCount = @(& git -C $dependencyPath status --porcelain=v1 --untracked-files=all).Count
        }
    }
    if (-not $exists -or -not $isGitRepository) { $success = $false }

    $impacted = $false
    foreach ($changedFile in $changedFiles) {
        if (Test-PolicyPathMatchesAnyPattern -Path $changedFile -Patterns @($dependency.impactPatterns)) { $impacted = $true }
    }
    $results += [ordered]@{
        repository = [string] $dependency.repository
        relationship = [string] $dependency.relationship
        exists = $exists
        isGitRepository = $isGitRepository
        impactedByCurrentChanges = $impacted
        defaultAccess = [string] $dependency.defaultAccess
        automaticVerification = [bool] $dependency.automaticVerification
        head = $head
        dirtyStatusCount = $dirtyStatusCount
        compatibilityVerified = $false
    }
}

$report = [ordered]@{
    repository = Get-PolicyRepositoryName -Policy $policy
    success = $success
    readOnly = $true
    changedFileCount = $changedFiles.Count
    dependencies = $results
    externalSystemsModified = $false
}
if ($Json) { $report | ConvertTo-Json -Depth 10 }
else {
    $report | ConvertTo-Json -Depth 10
    Write-Output 'Dependency inspection is read-only; compatibility builds were not executed.'
}
if (-not $success) { exit 1 }
