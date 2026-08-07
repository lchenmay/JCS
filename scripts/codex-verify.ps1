param([switch] $Full, [switch] $Json, [switch] $PolicyOnly)

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
. (Join-Path $repoRoot '.codex\hooks\VerificationCommon.ps1')
Reset-CodexVerificationResults
$quiet = [bool] $Json
$statusLines = @(git -C $repoRoot status --porcelain=v1 --untracked-files=all)

$generatedPattern = '(?i)(JCS\.Shared[\\/](OrmTypes|OrmMor|CustomMor|Types)\.fs|JCS\.Shared[\\/]sql(PostgreSQL|SQLServer)\.sql|BizShared[\\/](OrmTypes|OrmMor|CustomMor|Types)\.(fs|ts|d\.ts)|(?:portal|vscode|VsCodeTemplate)[\\/]src[\\/]lib[\\/]shared[\\/](OrmTypes\.d\.ts|OrmMor\.ts|CustomMor\.ts|Types\.d\.ts))'
$designPattern = '(?i)(JCS\.Shared[\\/]Design-.*\.json|BizShared[\\/]Design-.*\.json)'
$generatedChanges = @($statusLines | Where-Object { $_ -match $generatedPattern })
$designChanges = @($statusLines | Where-Object { $_ -match $designPattern })
if ($generatedChanges.Count -gt 0 -and $designChanges.Count -eq 0) {
    Add-CodexVerificationResult 'generated-file-consistency' $false 1 ("Generated files changed without an authoritative Design-*.json change:`n" + ($generatedChanges -join "`n"))
}
else {
    Add-CodexVerificationResult 'generated-file-consistency' $true 0 'Generated changes are absent or accompanied by an authoritative Design change.'
}

Invoke-CodexVerificationCheck -Name 'policy-tests' -WorkingDirectory $repoRoot -Executable 'pwsh.exe' -Arguments @('-NoProfile', '-File', (Join-Path $repoRoot '.codex\hooks\Test-Policy.ps1'), '-PolicyPath', (Join-Path $repoRoot '.codex\policy.json')) -Quiet:$quiet
if (-not $PolicyOnly) {
    Invoke-CodexVerificationCheck -Name 'typesys-release-build' -WorkingDirectory $repoRoot -Executable 'dotnet' -Arguments @('build', (Join-Path $repoRoot 'TypeSys\TypeSys.fsproj'), '-c', 'Release', '--no-restore') -Quiet:$quiet
    Invoke-CodexVerificationCheck -Name 'jcs-shared-release-build' -WorkingDirectory $repoRoot -Executable 'dotnet' -Arguments @('build', (Join-Path $repoRoot 'JCS.Shared\JCS.Shared.fsproj'), '-c', 'Release', '--no-restore') -Quiet:$quiet
    $bizLogicsChanged = $Full -or @($statusLines | Where-Object { $_ -match '(?i)^.. JCS\.BizLogics[\\/]' }).Count -gt 0
    if ($bizLogicsChanged) {
        Invoke-CodexVerificationCheck -Name 'jcs-bizlogics-release-build' -WorkingDirectory $repoRoot -Executable 'dotnet' -Arguments @('build', (Join-Path $repoRoot 'JCS.BizLogics\JCS.BizLogics.fsproj'), '-c', 'Release', '--no-restore') -Quiet:$quiet
    }
    $portalChanged = $Full -or @($statusLines | Where-Object { $_ -match '(?i)^.. portal[\\/]' }).Count -gt 0
    $vscodeChanged = $Full -or @($statusLines | Where-Object { $_ -match '(?i)^.. vscode[\\/]' }).Count -gt 0
    $templateChanged = $Full -or @($statusLines | Where-Object { $_ -match '(?i)^.. VsCodeTemplate[\\/]' }).Count -gt 0
    if ($portalChanged) { Invoke-CodexVerificationCheck -Name 'portal-typecheck' -WorkingDirectory (Join-Path $repoRoot 'portal') -Executable 'npm' -Arguments @('run', 'test') -Quiet:$quiet }
    if ($vscodeChanged) { Invoke-CodexVerificationCheck -Name 'vscode-typecheck' -WorkingDirectory (Join-Path $repoRoot 'vscode') -Executable 'npm' -Arguments @('run', 'test') -Quiet:$quiet }
    if ($templateChanged) { Invoke-CodexVerificationCheck -Name 'template-typecheck' -WorkingDirectory (Join-Path $repoRoot 'VsCodeTemplate') -Executable 'npm' -Arguments @('run', 'test') -Quiet:$quiet }
}

Write-CodexVerificationReport -Repository $repoRoot -ChangedFileCount $statusLines.Count -Full:$Full -PolicyOnly:$PolicyOnly -Json:$Json
if (-not $script:CodexVerificationSuccess) { exit 1 }
exit 0
