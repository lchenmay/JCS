@echo off
setlocal

set VUE_BUILD_PATH="dist"
set DEPLOY_MAIN_FOLDER="../VueDeploy"
set DEPLOY_GCHAIN_PATH="../GChainVsOpen/Deploy"
set DEPLOY_CTC_PATH="../CrypTradeClubVsOpen/Deploy"

set FORCE_FLAG=0

if "%~1"=="--force" set FORCE_FLAG=1

echo git reset fetch
git reset --hard HEAD
git fetch origin

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
git pull origin main
bun install
bun bd

echo robocopy
if %errorlevel% equ 0 (
    rmdir /s /q %DEPLOY_GCHAIN_PATH%
    rmdir /s /q %DEPLOY_CTC_PATH%

    robocopy /mt /z /purge /s %VUE_BUILD_PATH% %DEPLOY_GCHAIN_PATH%
    robocopy /mt /z /purge /s %VUE_BUILD_PATH% %DEPLOY_CTC_PATH%
)

:End_Script
@echo =======DEPLOY FIN=======
endlocal