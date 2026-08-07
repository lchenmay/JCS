param([switch] $Execute, [switch] $AllowUnchangedDesign)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$expectedRoot = 'D:\DEV\JCS'
if (-not [string]::Equals($repoRoot, $expectedRoot, [StringComparison]::OrdinalIgnoreCase)) {
    throw "Refusing generation outside $expectedRoot. Resolved root: $repoRoot"
}

$status = @(git -C $repoRoot status --porcelain=v1 --untracked-files=all)
$generatedPattern = '(?i)(JCS\.Shared[\\/](OrmTypes|OrmMor|CustomMor|Types)\.fs|JCS\.Shared[\\/]sql(PostgreSQL|SQLServer)\.sql|BizShared[\\/](OrmTypes|OrmMor|CustomMor|Types)\.(fs|ts|d\.ts)|(?:portal|vscode|VsCodeTemplate)[\\/]src[\\/]lib[\\/]shared[\\/](OrmTypes\.d\.ts|OrmMor\.ts|CustomMor\.ts|Types\.d\.ts))'
$generatedChanges = @($status | Where-Object { $_ -match $generatedPattern })
$designChanges = @($status | Where-Object { $_ -match '(?i)(JCS\.Shared|BizShared)[\\/]Design-.*\.json' })
$issues = New-Object System.Collections.Generic.List[string]
if ($generatedChanges.Count -gt 0) { $issues.Add("Generated outputs already have changes:`n$($generatedChanges -join "`n")") }
if (-not $AllowUnchangedDesign -and $designChanges.Count -eq 0) { $issues.Add('No authoritative Design-*.json change was detected.') }

$programPath = Join-Path $repoRoot 'TypeSys\Program.fs'
$program = Get-Content -Raw -LiteralPath $programPath
if ($program -notmatch 'TypeSys has no default target') { $issues.Add('TypeSys does not contain the no-default-target safety gate.') }
if ($program -notmatch [regex]::Escape('D:\DEV\JCS\JCS.Shared')) { $issues.Add('JCS target path is not D:\DEV\JCS\JCS.Shared.') }
if ($program -match '(?i)let\s+pwd\s*=\s*"[^"\r\n]+"') { $issues.Add('TypeSys still contains an embedded password literal.') }

Write-Output "JCS controlled TypeSys generation; execute=$Execute"
if ($issues.Count -gt 0) {
    $issues | ForEach-Object { Write-Output "BLOCKED: $_" }
    Write-Output 'No generator was executed and no generated output was modified.'
    if ($Execute) { exit 1 }
    exit 0
}
if (-not $Execute) { Write-Output 'Preflight passed. Re-run with -Execute.'; exit 0 }

Push-Location $repoRoot
try {
    & dotnet run --project (Join-Path $repoRoot 'TypeSys\TypeSys.fsproj') -c Release --no-restore -- --target 6
    if ($LASTEXITCODE -ne 0) { throw "TypeSys exited with $LASTEXITCODE." }
}
finally { Pop-Location }

& (Join-Path $repoRoot 'scripts\codex-verify.ps1') -Full
exit $LASTEXITCODE
