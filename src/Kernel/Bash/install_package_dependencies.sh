#!/usr/bin/env bash
set -euo pipefail

# Load the shared JSON‑safe pipeline
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/json_safe_pipeline.sh

PACKAGES=("$@")
ROLLBACK_LIST=""

# ---------------------------
# Argument validation
# ---------------------------
if [ $# -eq 0 ]; then
    json_fail "No packages specified"
fi

# ---------------------------
# Rollback logic
# ---------------------------
rollback() {
    if [ -n "$ROLLBACK_LIST" ]; then
        echo "Rolling back..." >&2
        apt-get remove -y $ROLLBACK_LIST || true
    fi
}

# Ensure rollback runs on failure
trap rollback ERR

# ---------------------------
# Main logic
# ---------------------------
echo "Updating package lists..." >&2
apt-get update -y

echo "Installing packages: ${PACKAGES[*]}" >&2
for pkg in "${PACKAGES[@]}"; do
    if dpkg -s "$pkg" >/dev/null 2>&1; then
        echo "$pkg already installed." >&2
    else
        apt-get install -y "$pkg"
        ROLLBACK_LIST="$ROLLBACK_LIST $pkg"
    fi
done

echo "All packages installed successfully." >&2
