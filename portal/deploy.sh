VUE_BUILD_PATH="dist"
DEPLOY_MAIN_FOLDER="../VueDeploy"
DEPLOY_GCHAIN_PATH="../GChainVsOpen/Deploy"
DEPLOY_CTC_PATH="../CrypTradeClubVsOpen/Deploy"

FORCE_FLAG=0
SKIP_GIT=0

if [ "$1" == "--fc" ]; then
    FORCE_FLAG=1
fi
if [ "$1" == "--skip-git" ]; then
    SKIP_GIT=1
fi

if [ "$SKIP_GIT" -eq 1 ]; then
    echo "GitHub 不可用，跳过 git 操作（代码已通过 scp 同步）"
    bun install
    bun bd
    if [ $? -eq 0 ]; then
        rm -rf "$DEPLOY_GCHAIN_PATH"
        rm -rf "$DEPLOY_CTC_PATH"
        mkdir -p "$DEPLOY_GCHAIN_PATH"
        mkdir -p "$DEPLOY_CTC_PATH"
        rsync -av --delete "$VUE_BUILD_PATH/" "$DEPLOY_GCHAIN_PATH/"
        rsync -av --delete "$VUE_BUILD_PATH/" "$DEPLOY_CTC_PATH/"
    fi
    echo "=======DEPLOY FIN (scp mode)======="
    exit 0
fi

git reset --hard HEAD
echo "git reset done"

if git fetch origin 2>/dev/null; then
    LATEST_COMMIT=$(git rev-parse origin/main)
    CURRENT_COMMIT=$(git rev-parse HEAD)
    GIT_OK=1
else
    echo "⚠ git fetch 失败，GitHub 可能不可用。如果代码已通过 scp 同步，用 --skip-git 重新部署"
    LATEST_COMMIT=""
    CURRENT_COMMIT=$(git rev-parse HEAD)
    GIT_OK=0
fi

execute_build_and_deploy() {
    set -x
    if [ "$GIT_OK" -eq 1 ]; then
        git pull origin main
    else
        echo "⚠ 跳过 git pull（GitHub 不可用）"
    fi
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
    if [ "$GIT_OK" -eq 0 ]; then
        echo "GitHub 不可用，使用本地代码构建"
        execute_build_and_deploy
    elif [ "$LATEST_COMMIT" != "$CURRENT_COMMIT" ]; then
        echo "Commits not same  OLD: $CURRENT_COMMIT - NEW: $LATEST_COMMIT"
        execute_build_and_deploy
    else
        echo "Commits: $CURRENT_COMMIT same."
    fi
fi



echo "=======DEPLOY FIN======="
