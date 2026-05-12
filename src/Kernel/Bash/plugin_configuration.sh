# plugin_configuration.sh
# source "/usr/lib/lunaticpanel/plugins/gamehost/bash/kernel/plugin_configuration.sh"

LUNATICPANEL_FOLDER_NAME="lunaticpanel"
LUNATICPANEL_PLUGINS_FOLDER_NAME="plugins"

LINUX_USR_FOLDER_NAME="usr"
LINUX_LIB_FOLDER_NAME="lib"
LINUX_ETC_FOLDER_NAME="etc"
LINUX_VAR_FOLDER_NAME="var"

BASH_FOLDER_NAME="bash"
CONFIG_FOLDER_NAME="config"
REPOS_FOLDER_NAME="repos"
DOWNLOAD_FOLDER_NAME="download"

HOME_FOLDER_NAME="home"
LGSM_FOLDER_NAME="lgsm"

lp_linux_assembly_name() {
  printf '%s' "$1" | tr '.' '_' | tr '[:upper:]' '[:lower:]'
}

lp_ensure_created() {
  local path="$1"
  local dir="$path"

  if [ -f "$path" ]; then
    dir="$(dirname "$path")"
  fi

  if [ ! -d "$dir" ]; then
    mkdir -p -m 755 "$dir"
  fi

  printf '%s\n' "$path"
}

lp_plugin_folder() {
  local linux_assembly
  linux_assembly="$(lp_linux_assembly_name "$1")"
  lp_ensure_created "/${LINUX_USR_FOLDER_NAME}/${LINUX_LIB_FOLDER_NAME}/${LUNATICPANEL_FOLDER_NAME}/${LUNATICPANEL_PLUGINS_FOLDER_NAME}/${linux_assembly}"
}

lp_plugin_etc_folder() {
  local linux_assembly
  linux_assembly="$(lp_linux_assembly_name "$1")"
  lp_ensure_created "/${LINUX_ETC_FOLDER_NAME}/${LUNATICPANEL_FOLDER_NAME}/${LUNATICPANEL_PLUGINS_FOLDER_NAME}/${linux_assembly}"
}

lp_plugin_var_folder() {
  local linux_assembly
  linux_assembly="$(lp_linux_assembly_name "$1")"
  lp_ensure_created "/${LINUX_VAR_FOLDER_NAME}/${LUNATICPANEL_FOLDER_NAME}/${LUNATICPANEL_PLUGINS_FOLDER_NAME}/${linux_assembly}"
}

lp_user_plugin_folder() {
  local linux_assembly
  linux_assembly="$(lp_linux_assembly_name "$1")"
  lp_ensure_created "/${HOME_FOLDER_NAME}/${LGSM_FOLDER_NAME}/${LUNATICPANEL_FOLDER_NAME}/${LUNATICPANEL_PLUGINS_FOLDER_NAME}/${linux_assembly}"
}

lp_user_config_folder() {
  lp_ensure_created "$(lp_user_plugin_folder "$1")/${CONFIG_FOLDER_NAME}"
}

lp_user_bash_folder() {
  lp_ensure_created "$(lp_user_plugin_folder "$1")/${BASH_FOLDER_NAME}"
}

lp_bash_folder() {
  lp_ensure_created "$(lp_plugin_folder "$1")/${BASH_FOLDER_NAME}"
}

lp_config_folder() {
  lp_ensure_created "$(lp_plugin_etc_folder "$1")/${CONFIG_FOLDER_NAME}"
}

lp_repos_folder() {
  lp_ensure_created "$(lp_plugin_etc_folder "$1")/${REPOS_FOLDER_NAME}"
}

lp_download_folder() {
  lp_ensure_created "$(lp_user_plugin_folder "$1")/${DOWNLOAD_FOLDER_NAME}"
}

lp_path_join_lower() {
  local out=""
  local part
  for part in "$@"; do
    [ -n "$part" ] || continue
    part="$(printf '%s' "$part" | tr '[:upper:]' '[:lower:]')"
    if [ -z "$out" ]; then
      out="$part"
    else
      out="${out}/${part}"
    fi
  done
  printf '%s\n' "$out"
}

lp_user_download_base() {
  local assembly="$1"; shift
  local module="$1"; shift
  lp_ensure_created "$(lp_download_folder "$assembly")/$(printf '%s' "$module" | tr '[:upper:]' '[:lower:]')"
}

lp_user_download_for() {
  local assembly="$1"; shift
  local module="$1"; shift
  local filename="$1"; shift
  local basePath="$(lp_user_download_base "$assembly" "$module")"
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    printf '%s\n' "$(lp_ensure_created "$basePath/$subpath")/$filename"
  else
    printf '%s\n' "$basePath/$filename"
  fi
}

lp_user_download_dir() {
  local assembly="$1"; shift
  local module="$1"; shift
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    lp_ensure_created "$(lp_user_download_base "$assembly" "$module")/${subpath}"
  else
    lp_ensure_created "$(lp_user_download_base "$assembly" "$module")"
  fi
}

lp_repos_base() {
  local assembly="$1"; shift
  local module="$1"; shift
  lp_ensure_created "$(lp_repos_folder "$assembly")/$(printf '%s' "$module" | tr '[:upper:]' '[:lower:]')"
}

lp_repos_for() {
  local assembly="$1"; shift
  local module="$1"; shift
  local repos_name="$1"; shift
  local basePath="$(lp_repos_base "$assembly" "$module")"
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    printf '%s\n' "$(lp_ensure_created "$basePath/$subpath")/$repos_name"
  else
    printf '%s\n' "$basePath/$repos_name"
  fi
}

lp_config_base() {
  local assembly="$1"; shift
  local module="$1"; shift
  lp_ensure_created "$(lp_config_folder "$assembly")/$(printf '%s' "$module" | tr '[:upper:]' '[:lower:]')"
}

lp_config_for() {
  local assembly="$1"; shift
  local module="$1"; shift
  local filename="$1"; shift
  local basePath="$(lp_config_base "$assembly" "$module")"
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    printf '%s\n' "$(lp_ensure_created "$basePath/$subpath")/$filename"
  else
    printf '%s\n' "$basePath/$filename"
  fi
}

lp_user_config_base() {
  local assembly="$1"; shift
  local module="$1"; shift
  lp_ensure_created "$(lp_user_config_folder "$assembly")/$(printf '%s' "$module" | tr '[:upper:]' '[:lower:]')"
}

lp_user_config_for() {
  local assembly="$1"; shift
  local module="$1"; shift
  local filename="$1"; shift
  local basePath="$(lp_user_config_base "$assembly" "$module")"
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    printf '%s\n' "$(lp_ensure_created "$basePath/$subpath")/$filename"
  else
    printf '%s\n' "$basePath/$filename"
  fi
}

lp_bash_base() {
  local assembly="$1"; shift
  local module="$1"; shift
  lp_ensure_created "$(lp_bash_folder "$assembly")/$(printf '%s' "$module" | tr '[:upper:]' '[:lower:]')"
}

lp_bash_for() {
  local assembly="$1"; shift
  local module="$1"; shift
  local filename="$1"; shift
  local basePath="$(lp_bash_base "$assembly" "$module")"
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    printf '%s\n' "$(lp_ensure_created "$basePath/$subpath")/$filename"
  else
    printf '%s\n' "$basePath/$filename"
  fi
}

lp_user_bash_base() {
  local assembly="$1"; shift
  local module="$1"; shift
  lp_ensure_created "$(lp_user_bash_folder "$assembly")/$(printf '%s' "$module" | tr '[:upper:]' '[:lower:]')"
}

lp_user_bash_for() {
  local assembly="$1"; shift
  local module="$1"; shift
  local filename="$1"; shift
  local basePath="$(lp_user_bash_base "$assembly" "$module")"
  local subpath
  subpath="$(lp_path_join_lower "$@")"
  if [ -n "$subpath" ]; then
    printf '%s\n' "$(lp_ensure_created "$basePath/$subpath")/$filename"
  else
    printf '%s\n' "$basePath/$filename"
  fi
}


lp_print_all_paths() {
  local assembly="$1"
  local module="$2"
  local filename="${3:-mock.txt}"

  local linux_assembly
  linux_assembly="$(lp_linux_assembly_name "$assembly")"

  printf '%s\n' "PLUGIN_FOLDER=$(lp_plugin_folder "$assembly")"
  printf '%s\n' "PLUGIN_ETC_FOLDER=$(lp_plugin_etc_folder "$assembly")"
  printf '%s\n' "PLUGIN_VAR_FOLDER=$(lp_plugin_var_folder "$assembly")"

  printf '%s\n' "USER_PLUGIN_FOLDER=$(lp_user_plugin_folder "$assembly")"
  printf '%s\n' "USER_CONFIG_FOLDER=$(lp_user_config_folder "$assembly")"
  printf '%s\n' "USER_BASH_FOLDER=$(lp_user_bash_folder "$assembly")"

  printf '%s\n' "CONFIG_BASE=$(lp_config_base "$assembly" "$module")"
  printf '%s\n' "CONFIG_FILE=$(lp_config_for "$assembly" "$module" "$filename")"

  printf '%s\n' "USER_CONFIG_BASE=$(lp_user_config_base "$assembly" "$module")"
  printf '%s\n' "USER_CONFIG_FILE=$(lp_user_config_for "$assembly" "$module" "$filename")"

  printf '%s\n' "BASH_BASE=$(lp_bash_base "$assembly" "$module")"
  printf '%s\n' "BASH_FILE=$(lp_bash_for "$assembly" "$module" "$filename")"

  printf '%s\n' "USER_BASH_BASE=$(lp_user_bash_base "$assembly" "$module")"
  printf '%s\n' "USER_BASH_FILE=$(lp_user_bash_for "$assembly" "$module" "$filename")"

  printf '%s\n' "REPOS_BASE=$(lp_repos_base "$assembly" "$module")"
  printf '%s\n' "REPOS_ENTRY=$(lp_repos_for "$assembly" "$module" "repos")"

  printf '%s\n' "USER_DOWNLOAD_FOLDER=$(lp_user_download_base "$assembly" "$module")"
  printf '%s\n' "USER_DOWNLOAD_FILE=$(lp_user_download_for "$assembly" "$module" "$filename")"
}
