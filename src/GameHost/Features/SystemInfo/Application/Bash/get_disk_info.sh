#!/usr/bin/env bash

path="$1"
[ -n "$path" ] || exit 1

# df in bytes for accuracy (filesystem space, not directory size)
read -r total used < <(df -B1 --output=size,used -- "$path" | awk 'NR==2{print $1, $2}')

# convert to GiB with decimals
used_gb=$(awk -v v="$used"  'BEGIN { printf "%.2f", v / 1024 / 1024 / 1024 }')
total_gb=$(awk -v v="$total" 'BEGIN { printf "%.2f", v / 1024 / 1024 / 1024 }')

echo "${used_gb};${total_gb}"
