#!/usr/bin/env bash
set -euo pipefail
set -E

source "/usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/plugin_configuration.sh"
ASSEMBLY_NAME="gamehost"
MODLIST_USER_CONFIG_BASE="$(lp_user_config_base "$ASSEMBLY_NAME" "modlist")"

load_modlist() {
    local id="$1"
    local file="${MODLIST_USER_CONFIG_BASE}/modlists/modlist-${id}.json"

    if [[ ! -f "$file" ]]; then
        echo "Error: File '$file' not found" >&2
        return 1
    fi

    # Declare associative array
    declare -gA MODLIST_DICT=()

    # Extract all part names under .Mods
    local parts
    parts=$(jq -r '.Mods | keys[]' "$file")

    # Loop through each part and store its JSON array
    local part json
    for part in $parts; do
        json=$(jq -c ".Mods[\"${part}\"]" "$file")
        MODLIST_DICT["$part"]="$json"
    done
}


