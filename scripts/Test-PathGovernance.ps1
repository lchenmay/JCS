param()

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$issues = New-Object System.Collections.Generic.List[string]

function Read-RepositoryFile([string] $relativePath) {
    Get-Content -Raw -LiteralPath (Join-Path $repoRoot $relativePath)
}

$program = Read-RepositoryFile 'TypeSys\Program.fs'
$codeRobot = Read-RepositoryFile 'TypeSys\CodeRobot.fs'
$aioServer = Read-RepositoryFile 'AioServer\Program.fs'
$publishProfile = Read-RepositoryFile 'BlazorWebAssembly\Properties\PublishProfiles\FolderProfile.pubxml'
$typeSysProject = Read-RepositoryFile 'TypeSys\TypeSys.fsproj'
$runtimePathSources = $program + $codeRobot + $aioServer + $publishProfile

if ($runtimePathSources -match '(?i)(?<![A-Za-z])[A-Z]:[\/]') {
    $issues.Add('A runtime or publishing source still contains a hardcoded drive path.')
}
if ($codeRobot -match 'File\.Copy\s*\(') {
    $issues.Add('TypeSys still copies hand-maintained frontend files.')
}
if ($codeRobot -notmatch 'legacy TypeSys\.short entry is disabled') {
    $issues.Add('The unsafe legacy TypeSys.short entry is not explicitly disabled.')
}
if ($program -notmatch 'repositoryDirectory repositoryRoot') {
    $issues.Add('The JCS generator target is not repository-relative.')
}
if ($program -notmatch 'TYPESYS_AIARWA_ROOT') {
    $issues.Add('The cross-repository generator target lacks an explicit root gate.')
}
if ($aioServer -notmatch 'JCS_AIOSERVER_WEB_ROOT') {
    $issues.Add('AioServer does not expose an explicit web-root override.')
}
if ($publishProfile -notmatch '\$\(MSBuildProjectDirectory\)') {
    $issues.Add('The Blazor folder publish profile is not project-relative.')
}
if ($typeSysProject -match 'set\.json' -or (Test-Path -LiteralPath (Join-Path $repoRoot 'TypeSys\set.json'))) {
    $issues.Add('The obsolete path-bound TypeSys set.json is still packaged or tracked.')
}

if ($issues.Count -gt 0) {
    $issues | ForEach-Object { Write-Output "FAIL: $_" }
    exit 1
}

Write-Output 'Path governance contract: PASS'
exit 0
