#!/usr/bin/env bash
set -euo pipefail
set -E

LOCKFILE="/tmp/lgs_game_server_install.lock"
exec 9>"$LOCKFILE" || exit 1
flock -n 9 || exit 1

source "/usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/plugin_configuration.sh"

LGSM_USER="lgsm"
LGSM_HOME="/home/$LGSM_USER"
SUDOERS_FILE="/etc/sudoers.d/lgsm"

if [ $# -ne 2 ]; then
    echo "Missing arguments: <game_server> <display_name>" >&2
    exit 1
fi

GAME_SERVER="$1"
DISPLAY_NAME="$2"
ASSEMBLY_NAME="gamehost"

PLUGIN_DIR="/usr/lib/lunaticpanel/plugins/gamehost"
PLUGIN_BASH_BASE="$PLUGIN_DIR/bash"
PLUGIN_BASH_KERNEL_BASE="$PLUGIN_BASH_BASE/kernel"
PLUGIN_BASH_LGS_BASE="$PLUGIN_BASH_BASE/linuxgameserver"
PLUGIN_BASH_KERNEL_FILEWRITER="$PLUGIN_BASH_KERNEL_BASE/file_writer_method.sh"
PLUGIN_BASH_LGS_LOCALE_SCRIPT="$PLUGIN_BASH_LGS_BASE/set_local_culture.sh"

source "$PLUGIN_BASH_KERNEL_FILEWRITER"

STATE_DIR="/etc/lunaticpanel/plugins/gamehost/config/linuxgameserver"
PROGRESS_FILE="$STATE_DIR/installation_progress_state.json"
FINAL_FILE="$STATE_DIR/installation_state.json"

AVAILABLE_GAMES_DIR="/etc/lunaticpanel/plugins/gamehost/repos/linuxgameserver/available_games/games"
GAME_REPO_PATH="$AVAILABLE_GAMES_DIR/$GAME_SERVER"
TARGET_CONTROL_DIR="/home/lgsm/lunaticpanel/plugins/gamehost/bash/lifecycle/server_control"
DEPENDENCY_SCRIPT="$PLUGIN_BASH_KERNEL_BASE/install_package_dependencies.sh"

KERNEL_BASH_BASE="$(lp_bash_base "$ASSEMBLY_NAME" "kernel")"

FIX_HOME_PERM_SCRIPT="$KERNEL_BASH_BASE/fix_home_permission.sh"

if [ ! -d "$GAME_REPO_PATH" ]; then
    echo "Cannot install unsupported server by the panel, unless you forgot to fetch/clone the panel repos of supported game scripts." >&2
    exit 1
fi


bash "$PLUGIN_BASH_LGS_LOCALE_SCRIPT"

mkdir -p "$TARGET_CONTROL_DIR"

mkdir -p "$STATE_DIR"
chmod -R 755 "$STATE_DIR"

if [ -f "$FINAL_FILE" ]; then
    echo "Server for $DISPLAY_NAME is already installed" >&2
    exit 1
fi



write_progress() {
    local content
    content="$(jq -n \
        --arg step "Installing Server for $DISPLAY_NAME..." \
        --arg id "$GAME_SERVER" \
        --arg name "$DISPLAY_NAME" \
        '{
            FailureReason: null,
            IsInstalling: true,
            CurrentStep: $step,
            Id: $id,
            DisplayName: $name
        }'
    )"

    write_file "$PROGRESS_FILE" "$content"
    chmod -R 755 "$STATE_DIR"
}

write_failure() {
    local reason="$1"
    local content
    content="$(jq -n \
        --arg reason "$reason" \
        --arg step "Installing Server for $DISPLAY_NAME..." \
        --arg id "$GAME_SERVER" \
        --arg name "$DISPLAY_NAME" \
        '{
            FailureReason: $reason,
            IsInstalling: false,
            CurrentStep: $step,
            Id: $id,
            DisplayName: $name
        }'
    )"

    write_file "$PROGRESS_FILE" "$content"
    chmod -R 755 "$STATE_DIR"
}

write_final_file() {
    local content
    content="$(jq -n \
        --arg id "$GAME_SERVER" \
        --arg name "$DISPLAY_NAME" \
        --arg date "$(date -u +"%Y-%m-%dT%H:%M:%SZ")" \
        '{
            Id: $id,
            DisplayName: $name,
            InstallDate: $date
        }'
    )"

    write_file "$FINAL_FILE" "$content"
}


write_progress
ls -l "$PROGRESS_FILE">&2 || echo "NO PROGRESS FILE FROM INSIDE SCRIPT" >&2


SUCCESS=0

cleanup() {

    # ALWAYS remove sudo perms
    rm -f "$SUDOERS_FILE" || true
    # On success, delete progress file
    if [ "$SUCCESS" -eq 1 ]; then
        rm -f "$PROGRESS_FILE" || true
    fi

    exec 9>&-
    rm -f "$LOCKFILE"
}
trap cleanup EXIT

# ERR trap: write failure, keep progress file, still remove sudo perms
trap '
    trap - EXIT
    write_failure "Installation failed"
    cleanup
    exit 1
' ERR

# Clean Steam Cache from Previous Install Attempts

if [ -d "/home/lgsm/.steam/steamcmd" ]; then
    rm -rf "/home/lgsm/.steam/steamcmd/"* || true
fi

echo "$LGSM_USER ALL=(ALL) NOPASSWD:ALL" > "$SUDOERS_FILE"
chmod 440 "$SUDOERS_FILE"

bash "$DEPENDENCY_SCRIPT" curl locales >&2

runuser -l "$LGSM_USER" -c '
    set -euo pipefail
    set -E
    export DEBIAN_FRONTEND=noninteractive
    cd \"$LGSM_HOME\"
    curl -Lo linuxgsm.sh https://linuxgsm.sh
    chmod +x linuxgsm.sh
    bash ./linuxgsm.sh \"$GAME_SERVER\"
    bash \"./$GAME_SERVER\" auto-install
' >&2


write_final_file

chmod -R 755 "$STATE_DIR"


cp -a "$GAME_REPO_PATH"/. "$TARGET_CONTROL_DIR"/

bash -e "$FIX_HOME_PERM_SCRIPT"
SUCCESS=1


