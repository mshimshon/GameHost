#!/usr/bin/env bash
set -euo pipefail
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/json_safe_pipeline.sh
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/plugin_configuration.sh
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/file_writer_method.sh

LOCKFILE="/tmp/user_startup_params.lock"
exec 9>"$LOCKFILE" || json_fail "failed to open startup params lockfile"
flock -n 9 || json_fail "someone else is already writing changes to startup params"

if [ $# -lt 2 ]; then
    json_fail "missing arguments: <key> <value>"
fi

KEY="$1"
VALUE="$2"

USER_CONFIG_FILE="$(lp_user_config_for "gamehost" "lifecycle" "user_defined_startup_params.json")"


if [ ! -f "$USER_CONFIG_FILE" ]; then
    write_file "$USER_CONFIG_FILE" "[]" 755
fi

if [ -z "$VALUE" ]; then
    tmp="$(jq --arg key "$KEY" 'map(select(.Key != $key))' "$USER_CONFIG_FILE")"
else
    tmp="$(jq --arg key "$KEY" --arg value "$VALUE" '
        if any(.[]; .Key == $key) then
            map(if .Key == $key then .Value = $value else . end)
        else
            . + [{ Key: $key, Value: $value }]
        end
    ' "$USER_CONFIG_FILE")"
fi

write_file "$USER_CONFIG_FILE" "$tmp" 755

