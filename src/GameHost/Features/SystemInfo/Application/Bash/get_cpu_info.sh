#!/usr/bin/env bash

read -r cpu u1 n1 s1 i1 w1 irq1 sirq1 st1 _ < /proc/stat
total1=$((u1 + n1 + s1 + i1 + w1 + irq1 + sirq1 + st1))

sleep 1

read -r cpu u2 n2 s2 i2 w2 irq2 sirq2 st2 _ < /proc/stat
total2=$((u2 + n2 + s2 + i2 + w2 + irq2 + sirq2 + st2))

idle_delta=$((i2 - i1))
total_delta=$((total2 - total1))

if [ "$total_delta" -le 0 ]; then
  cpu_usage="0.00"
else
  cpu_usage=$(awk -v i="$idle_delta" -v t="$total_delta" 'BEGIN { printf "%.2f", (1 - (i / t)) * 100 }')
fi

cores=$(nproc)
model=$(awk -F ': ' '/model name/ {print $2; exit}' /proc/cpuinfo)

echo "${cpu_usage};${cores};${model}"
