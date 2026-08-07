param(
    [Parameter(Mandatory = $true)]
    [ValidateSet('audit', 'enforce')]
    [string] $Mode
)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
. (Join-Path $repoRoot '.codex\hooks\PolicyCommon.ps1')
$policy = Get-PolicyData -PolicyPath $policyPath
$previous = Get-PolicyMode -Policy $policy
$policy.mode = $Mode
$policy | ConvertTo-Json -Depth 24 | Set-Content -LiteralPath $policyPath -Encoding UTF8
Write-Output "$(Get-PolicyRepositoryName -Policy $policy) project policy mode: $previous -> $Mode"
Write-Output "Start a new Codex task rooted at $(Get-PolicyRepositoryRoot -Policy $policy), then review the changed Hook hash with /hooks."
