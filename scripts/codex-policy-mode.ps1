param(
    [Parameter(Mandatory = $true)]
    [ValidateSet('audit', 'enforce')]
    [string] $Mode
)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$policyPath = Join-Path $repoRoot '.codex\policy.json'
$policy = Get-Content -Raw -LiteralPath $policyPath | ConvertFrom-Json
$previous = [string] $policy.mode
$policy.mode = $Mode
$policy | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath $policyPath -Encoding utf8
Write-Output "JCS policy mode: $previous -> $Mode"
Write-Output 'Open a new Codex task rooted at D:\DEV\JCS to test the selected mode.'
