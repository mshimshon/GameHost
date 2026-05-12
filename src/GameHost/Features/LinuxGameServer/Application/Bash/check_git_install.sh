#!/usr/bin/env bash
set -euo pipefail
# Load the shared JSON‑safe pipeline
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/json_safe_pipeline.sh

# -------------------------
# Your logic here
# -------------------------

if ! command -v git >/dev/null 2>&1; then
    echo "This is a fake error message" >&2
    
    json_fail "git is not installed or not in PATH"
fi
echo "SUCCESS"

# If we reach this point, the wrapper will emit success JSON automatically 
