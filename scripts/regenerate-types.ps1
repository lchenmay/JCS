param([switch] $Execute, [switch] $AllowUnchangedDesign)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
foreach ($marker in @('.git', 'AGENTS.md', 'TypeSys\TypeSys.fsproj')) {
    if (-not (Test-Path -LiteralPath (Join-Path $repoRoot $marker))) {
        throw "Refusing generation because repository marker is missing: $marker"
    }
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
$codeRobot = Get-Content -Raw -LiteralPath (Join-Path $repoRoot 'TypeSys\CodeRobot.fs')
if ($program -notmatch 'TypeSys has no default target') { $issues.Add('TypeSys does not contain the no-default-target safety gate.') }
if ($program -notmatch 'repositoryDirectory repositoryRoot') { $issues.Add('JCS target is not resolved relative to the repository.') }
if ($program -notmatch 'TYPESYS_AIARWA_ROOT') { $issues.Add('The cross-repository Aiarwa target lacks an explicit root gate.') }
if (($program + $codeRobot) -match '(?i)[A-Z]:[\\/]') { $issues.Add('TypeSys still contains a hardcoded drive path.') }
if ($codeRobot -match '(?i)Password\s*=') { $issues.Add('TypeSys still contains an embedded password assignment.') }
if ($codeRobot -match 'File\.Copy\s*\(') { $issues.Add('TypeSys still copies hand-maintained frontend templates.') }

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
