using System.Diagnostics.CodeAnalysis;

namespace GameHost.Core;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class PluginKeys
{
    public static class Events
    {
        public const string OnBeforeRuntimeInitialization = $"{GameHostKeys.EVENT_PREFIX}.{GameHostKeys.ASSEMBLY_NAME}.{GameHostKeys.API_V1}.{nameof(PluginKeys)}.{nameof(Events)}.{nameof(OnBeforeRuntimeInitialization)}";
        public const string OnAfterRuntimeInitialization = $"{GameHostKeys.EVENT_PREFIX}.{GameHostKeys.ASSEMBLY_NAME}.{GameHostKeys.API_V1}.{nameof(PluginKeys)}.{nameof(Events)}.{nameof(OnAfterRuntimeInitialization)}";

    }
}
