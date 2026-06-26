@echo off
setlocal

set VUE_BUILD_PATH="dist"
set DEPLOY_MAIN_FOLDER="../VueDeploy"
set DEPLOY_GCHAIN_PATH="../GChainVsOpen/Deploy"
set DEPLOY_CTC_PATH="../CrypTradeClubVsOpen/Deploy"

set FORCE_FLAG=0
set SKIP_GIT=0
set GIT_OK=0

if "%~1"=="--force" set FORCE_FLAG=1
if "%~1"=="--skip-git" set SKIP_GIT=1

if "%SKIP_GIT%"=="1" (
    echo GitHub unavailable, skipping git (code synced via scp)
    call bun install
    call bun bd
    if %errorlevel% equ 0 (
        robocopy "%VUE_BUILD_PATH%" "%DEPLOY_GCHAIN_PATH%" /E /PURGE /NFL /NDL /NJH /NJS
        robocopy "%VUE_BUILD_PATH%" "%DEPLOY_CTC_PATH%" /E /PURGE /NFL /NDL /NJH /NJS
    )
    @echo =======DEPLOY FIN (scp mode)=======
    endlocal
    exit /b 0
)

echo git reset fetch
git reset --hard HEAD
git fetch origin 2>nul
if %errorlevel% neq 0 (
    echo WARNING: git fetch failed, GitHub may be unavailable
    echo If code was synced via scp, run with --skip-git
    set GIT_OK=0
    goto Execute_Build_n_Deploy
)
set GIT_OK=1

for /f %%i in ('git rev-parse origin/main') do set LATEST_COMMIT=%%i
for /f %%i in ('git rev-parse HEAD') do set CURRENT_COMMIT=%%i

if "%FORCE_FLAG%"=="1" (
    goto Force_Execute
) else (
    if "%LATEST_COMMIT%" neq "%CURRENT_COMMIT%" (
        echo Commits not same  OLD: %CURRENT_COMMIT% - NEW: %LATEST_COMMIT%
        goto Execute_Build_n_Deploy
    ) else (
        echo Commits:%CURRENT_COMMIT% same.
    )
)

goto End_Script

:Force_Execute
echo Force flag (--force) detected. Executing commands unconditionally.

:Execute_Build_n_Deploy
echo git pull build
if "%GIT_OK%"=="1" (
    git pull origin main
) else (
    echo Skipping git pull (GitHub unavailable)
)
call bun install
call bun bd

echo robocopy
if %errorlevel% equ 0 (
    robocopy "%VUE_BUILD_PATH%" "%DEPLOY_GCHAIN_PATH%" /E /PURGE /NFL /NDL /NJH /NJS
    robocopy "%VUE_BUILD_PATH%" "%DEPLOY_CTC_PATH%" /E /PURGE /NFL /NDL /NJH /NJS
)

:End_Script
@echo =======DEPLOY FIN=======
endlocal
