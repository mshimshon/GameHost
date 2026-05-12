#!/bin/bash

USER_NAME="lgsm"

if id "$USER_NAME" >/dev/null 2>&1; then
  echo "True"
else
  echo "False"
fi