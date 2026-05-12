#!/usr/bin/env bash
set -euo pipefail
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/json_safe_pipeline.sh

LGSM_USER="lgsm"
LGSM_HOME="/home/$LGSM_USER"

# 1. Do nothing if user exists
if id "$LGSM_USER" >/dev/null 2>&1; then
    exit 0
fi

# 2. Remove home dir if it exists
if [ -d "$LGSM_HOME" ]; then
    rm -rf "$LGSM_HOME"
fi

# 3. Create the user with a fresh home directory
useradd -m -d "$LGSM_HOME" -s /bin/bash "$LGSM_USER"
passwd -d "$LGSM_USER"
