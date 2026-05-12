#!/usr/bin/env bash

read -r total used < <(free -b | awk '/^Mem:/ {print $2, $3}')

used_gb=$(awk -v v="$used" 'BEGIN { printf "%.2f", v / 1024 / 1024 / 1024 }')
total_gb=$(awk -v v="$total" 'BEGIN { printf "%.2f", v / 1024 / 1024 / 1024 }')

echo "${used_gb};${total_gb}"