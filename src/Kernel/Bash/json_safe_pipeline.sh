
#!/usr/bin/env bash
# jsonwrap.sh
# Provides JSON-safe stdout isolation and helper functions using jq

# Redirect stdout → stderr, preserve original stdout in FD3

# Neutralize inherited functions
unset -f exit || true
unset -f source || true
unset -f . || true
IFS=$' \t\n'

# State
__JSON_COMPLETED=true
__JSON_FAILURE_MESSAGE=""

json_fail() {
    __JSON_COMPLETED=false
    __JSON_FAILURE_MESSAGE="$1"
    exit 1
}

json_emit_failure() {
    local msg="$1"
    jq -n --arg msg "$msg" '{completed:false, failure_message:$msg}' >&2
}

json_emit_success() {
    local payload
    payload=$(jq -n '{completed:true, failure_message:""}')
    #printf "%s\n\n" "$payload"
   printf -- '%s\n' '{"key":"value"}'
    #printf -- '%s\n' 'true'
}

json_cleanup() {
    if [ "$__JSON_COMPLETED" = true ]; then
        json_emit_success
    else
        json_emit_failure "$__JSON_FAILURE_MESSAGE"
    fi
}

# Install traps
trap json_cleanup EXIT
trap 'echo "ERROR: ${BASH_COMMAND}" >&2; json_fail "Script failed"' ERR
