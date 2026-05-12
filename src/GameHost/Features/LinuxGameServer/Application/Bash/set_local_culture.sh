#!/usr/bin/env bash
set -euo pipefail

# Load the shared JSON‑safe pipeline
. /usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/json_safe_pipeline.sh

# -------------------------
# Locale configuration
# -------------------------
sed -i 's/^# *en_US.UTF-8 UTF-8/en_US.UTF-8 UTF-8/' /etc/locale.gen
locale-gen en_US.UTF-8
echo 'LANG=en_US.UTF-8' > /etc/default/locale

export LANG=en_US.UTF-8
export LC_ALL=en_US.UTF-8
