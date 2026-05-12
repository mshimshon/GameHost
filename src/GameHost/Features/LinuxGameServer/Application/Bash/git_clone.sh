#!/usr/bin/env bash
set -euo pipefail

. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/json_safe_pipeline.sh

GIT_URL="${1:-}"
TARGET_DIR="${2:-}"

if [[ -z "$GIT_URL" ]]; then
    json_fail "Missing required argument: git URL"
fi

TMP_ERR="$(mktemp)"

# 2. Clean the folder (if provided)
if [[ -n "${TARGET_DIR:-}" ]]; then
    if [[ -d "$TARGET_DIR" ]]; then
        if ! rm -rf "$TARGET_DIR" 2>"$TMP_ERR"; then
            ERRMSG="$(cat "$TMP_ERR")"
            rm -f "$TMP_ERR"
            json_fail "Failed to clear target directory: $ERRMSG"
        fi
    fi
fi

# 3. Git clone
if [[ -n "${TARGET_DIR:-}" ]]; then
    if ! git clone "$GIT_URL" "$TARGET_DIR" >/dev/null 2>"$TMP_ERR"; then
        ERRMSG="$(cat "$TMP_ERR")"
        rm -f "$TMP_ERR"
        json_fail "$ERRMSG"
    fi
else
    if ! git clone "$GIT_URL" >/dev/null 2>"$TMP_ERR"; then
        ERRMSG="$(cat "$TMP_ERR")"
        rm -f "$TMP_ERR"
        json_fail "$ERRMSG"
    fi
fi

rm -f "$TMP_ERR"

chmod -R 755 "$TARGET_DIR"
