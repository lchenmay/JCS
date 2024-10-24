@REM make sure current dir is VsCodeOpen.


set VUE_BUILD_PATH="dist"
set DEPLOY_MAIN_FOLDER="../VueDeploy"

robocopy /mt /z /purge /s %VUE_BUILD_PATH% %DEPLOY_MAIN_FOLDER%
mklink /J %DEPLOY_GCHAIN_PATH% %DEPLOY_MAIN_FOLDER%
mklink /J %DEPLOY_CTC_PATH% %DEPLOY_MAIN_FOLDER%