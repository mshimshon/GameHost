namespace GameHost.Core.Features;

public static class ModListKeys
{
    public const string MODULE_NAME = "modlist";
    public const string USER_SAVED_MODLIST_FOLDER_NAME = "modlists";
    public static class Queries { }
    public static class Engine { }
    public static class Events
    {
        public const string ON_FILES_IN_MONITORED_MOD_LIST_FOLDER_CHANGED = $"{GameHostKeys.EVENT_PREFIX}.{GameHostKeys.ASSEMBLY_NAME}.{GameHostKeys.API_V1}.{nameof(ModListKeys)}.{nameof(Events)}.{nameof(ON_FILES_IN_MONITORED_MOD_LIST_FOLDER_CHANGED)}";

    }
}