using System.Diagnostics.CodeAnalysis;

namespace GameHost.Core;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class PluginKeys
{
    public static class Events
    {
        public const string OnBeforeRuntimeInitialization = $"{BaseInfo.AssemblyName}.{nameof(PluginKeys)}.{nameof(Events)}.{nameof(OnBeforeRuntimeInitialization)}";
        public const string OnAfterRuntimeInitialization = $"{BaseInfo.AssemblyName}.{nameof(PluginKeys)}.{nameof(Events)}.{nameof(OnAfterRuntimeInitialization)}";

    }
}
