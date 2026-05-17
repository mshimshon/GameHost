using System.Diagnostics.CodeAnalysis;

namespace GameHost.Core.Features;

[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public static class SystemInfoKeys
{
    public const string MODULE_NAME = "systeminfo";
    public static class Events
    {
        /// <summary>
        /// Raised when system resource information changes (CPU, RAM, or disk usage).
        /// </summary>
        public const string OnStateUpdate = $"{BaseInfo.ASSEMBLY_NAME}.{nameof(SystemInfoKeys)}.{nameof(Events)}.{nameof(OnStateUpdate)}";

        /// <summary>
        /// Triggers the update of the system information
        /// </summary>
        public const string UpdateInformation = $"{BaseInfo.ASSEMBLY_NAME}.{nameof(SystemInfoKeys)}.{nameof(Events)}.{nameof(UpdateInformation)}";
    }
}
