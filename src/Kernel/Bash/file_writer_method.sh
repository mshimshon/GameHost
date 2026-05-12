#!/usr/bin/env bash

# Write content to a temporary file and atomically replace the target.
# Usage: write_file <path> <content> <perm> <owner> 

write_file() {
    local filepath="$1"
    local content="$2"
    local owner="${4:-}"
    local perm="${3:-}"
    local tmpfile

    mkdir -p "$(dirname "$filepath")"
    tmpfile="$(mktemp "${filepath}.tmp.XXXXXX")"

    printf "%s" "$content" > "$tmpfile"

    if [ -n "$owner" ]; then
        chown "$owner" "$tmpfile"
    fi

    if [ -n "$perm" ]; then
        chmod "$perm" "$tmpfile"
    fi

    mv -f "$tmpfile" "$filepath"
}
