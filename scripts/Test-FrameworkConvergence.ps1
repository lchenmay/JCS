param()

$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).Path
$projects = @(
    'AioServer\AioServer.fsproj',
    'BizShared\BizShared.fsproj',
    'BlazorWebAssembly\BlazorWebAssembly.csproj',
    'JCS.BizLogics\JCS.BizLogics.fsproj',
    'JCS.Shared\JCS.Shared.fsproj',
    'Server\Server.fsproj',
    'TypeSys\TypeSys.fsproj',
    'WebLogics\WebLogics.fsproj'
)
$issues = New-Object System.Collections.Generic.List[string]

foreach ($relativePath in $projects) {
    $projectPath = Join-Path $repoRoot $relativePath
    $targetFramework = @(& dotnet msbuild $projectPath -nologo -getProperty:TargetFramework) | Select-Object -Last 1
    if ($LASTEXITCODE -ne 0 -or $targetFramework -ne 'net10.0') {
        $issues.Add("$relativePath resolves TargetFramework='$targetFramework', expected net10.0.")
    }
}

$serverProject = Get-Content -Raw -LiteralPath (Join-Path $repoRoot 'Server\Server.fsproj')
if ($serverProject -match 'UtilWebServer' -or $serverProject -notmatch 'UtilKestrel') {
    $issues.Add('Server is not bound exclusively to the current UtilKestrel server stack.')
}

$blazorProject = Get-Content -Raw -LiteralPath (Join-Path $repoRoot 'BlazorWebAssembly\BlazorWebAssembly.csproj')
if ($blazorProject -notmatch 'AspNetCorePackageVersion') {
    $issues.Add('Blazor Microsoft package versions are not controlled centrally.')
}

if ($issues.Count -gt 0) {
    $issues | ForEach-Object { Write-Output "FAIL: $_" }
    exit 1
}

Write-Output 'Framework convergence contract: PASS'
exit 0
