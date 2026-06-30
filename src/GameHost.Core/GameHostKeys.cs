namespace GameHost.Core;

public static class GameHostKeys
{
    public const string ASSEMBLY_NAME = "GameHost";
    internal const string EVENT_PREFIX = $"eventkey.[{ASSEMBLY_NAME}]";
    internal const string ENGINE_PREFIX = $"enginekey.[{ASSEMBLY_NAME}]";
    internal const string QUERY_PREFIX = $"querykey.[{ASSEMBLY_NAME}]";

    internal const string EVENT_PREFIX_V1 = $"{EVENT_PREFIX}.{V1}";
    internal const string ENGINE_PREFIX_V1 = $"{ENGINE_PREFIX}.{V1}";
    internal const string QUERY_PREFIX_V1 = $"{QUERY_PREFIX}.{V1}";
    internal const string V1 = "v1";
    public static class Events
    {
        public const string ON_BEFORE_RUNTIME_INITIALIZATION = $"{EVENT_PREFIX_V1}.{nameof(PluginKeys)}.{nameof(Events)}.{nameof(ON_BEFORE_RUNTIME_INITIALIZATION)}";
        public const string ON_AFTER_RUNTIME_INITIALIZATION = $"{EVENT_PREFIX_V1}.{nameof(PluginKeys)}.{nameof(Events)}.{nameof(ON_AFTER_RUNTIME_INITIALIZATION)}";

    }
}