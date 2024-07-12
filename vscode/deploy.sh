VUE_BUILD_PATH="dist"
DEPLOY_MAIN_FOLDER="../VueDeploy"
DEPLOY_GCHAIN_PATH="../GChainVsOpen/Deploy"
DEPLOY_CTC_PATH="../CrypTradeClubVsOpen/Deploy"

FORCE_FLAG=0

if [ "$1" == "--fc" ]; then
    FORCE_FLAG=1
fi

git reset --hard HEAD
echo "git reset done"

git fetch origin
LATEST_COMMIT=$(git rev-parse origin/main)
CURRENT_COMMIT=$(git rev-parse HEAD)

execute_build_and_deploy() {
    set -x
    git pull origin main
    bun install
    bun bd
    set +x

    if [ $? -eq 0 ]; then
        set -x
        rm -rf "$DEPLOY_GCHAIN_PATH"
        rm -rf "$DEPLOY_CTC_PATH"

        mkdir -p "$DEPLOY_GCHAIN_PATH"
        mkdir -p "$DEPLOY_CTC_PATH"

        rsync -av --delete "$VUE_BUILD_PATH/" "$DEPLOY_GCHAIN_PATH/"
        rsync -av --delete "$VUE_BUILD_PATH/" "$DEPLOY_CTC_PATH/"
        set +x
    fi
}

if [ "$FORCE_FLAG" -eq 1 ]; then
    echo "Force flag (--fc) detected. Executing commands unconditionally."
    execute_build_and_deploy
else
    if [ "$LATEST_COMMIT" != "$CURRENT_COMMIT" ]; then
        echo "Commits not same  OLD: $CURRENT_COMMIT - NEW: $LATEST_COMMIT"
        execute_build_and_deploy
    else
        echo "Commits: $CURRENT_COMMIT same."
    fi
fi



echo "=======DEPLOY FIN======="