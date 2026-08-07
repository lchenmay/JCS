param(
    [Parameter(Mandatory = $true)] [string] $ResultsPath,
    [string] $CasesPath = '',
    [switch] $Json
)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
if ([string]::IsNullOrWhiteSpace($CasesPath)) { $CasesPath = Join-Path $repoRoot 'evals\controlled-system\cases.json' }
$spec = Get-Content -Raw -LiteralPath $CasesPath | ConvertFrom-Json
$rows = @(Get-Content -LiteralPath $ResultsPath | Where-Object { -not [string]::IsNullOrWhiteSpace($_) } | ForEach-Object { $_ | ConvertFrom-Json })
$expectedCount = @($spec.cases).Count * @($spec.variants).Count
if ($rows.Count -ne $expectedCount) { throw "Expected $expectedCount result rows, found $($rows.Count)." }

$keys = @{}
foreach ($row in $rows) {
    $key = "$($row.caseId)|$($row.variant)"
    if ($keys.ContainsKey($key)) { throw "Duplicate result: $key" }
    $keys[$key] = $true
}
foreach ($case in @($spec.cases)) {
    foreach ($variant in @($spec.variants)) {
        if (-not $keys.ContainsKey("$($case.id)|$variant")) { throw "Missing result: $($case.id)|$variant" }
    }
}

function Get-Median([double[]] $Values) {
    $sorted = @($Values | Sort-Object)
    if ($sorted.Count -eq 0) { return $null }
    $middle = [math]::Floor($sorted.Count / 2)
    if ($sorted.Count % 2 -eq 1) { return $sorted[$middle] }
    return ($sorted[($middle - 1)] + $sorted[$middle]) / 2
}
function Get-Wilson([int] $Successes, [int] $Total) {
    if ($Total -eq 0) { return [pscustomobject]@{ low = $null; high = $null } }
    $z = 1.95996398454005
    $p = $Successes / $Total
    $denominator = 1 + ($z * $z / $Total)
    $center = ($p + ($z * $z / (2 * $Total))) / $denominator
    $margin = ($z / $denominator) * [math]::Sqrt(($p * (1 - $p) / $Total) + ($z * $z / (4 * $Total * $Total)))
    return [pscustomobject]@{ low = ($center - $margin); high = ($center + $margin) }
}

$summary = foreach ($variant in @($spec.variants)) {
    $subset = @($rows | Where-Object { $_.variant -eq $variant })
    $successes = @($subset | Where-Object { [bool] $_.success }).Count
    $interval = Get-Wilson $successes $subset.Count
    [ordered]@{
        variant = $variant
        runs = $subset.Count
        successRate = $successes / $subset.Count
        successWilson95 = @($interval.low, $interval.high)
        unsafeSideEffects = @($subset | Where-Object { [bool] $_.unsafeSideEffect }).Count
        invalidCompletionClaims = @($subset | Where-Object { -not [bool] $_.completionClaimValid }).Count
        baselinePreservationRate = @($subset | Where-Object { [bool] $_.baselinePreserved }).Count / $subset.Count
        medianElapsedMs = Get-Median @($subset.elapsedMs)
        medianFirstMergeReadyMs = Get-Median @($subset.firstMergeReadyMs)
        medianTokens = Get-Median @($subset.tokens)
        totalUserInterventions = ($subset | Measure-Object -Property userInterventions -Sum).Sum
    }
}
$report = [ordered]@{ schemaVersion = 1; expectedRuns = $expectedCount; actualRuns = $rows.Count; variants = @($summary) }
if ($Json) { $report | ConvertTo-Json -Depth 8 } else { $summary | Format-Table -AutoSize }
