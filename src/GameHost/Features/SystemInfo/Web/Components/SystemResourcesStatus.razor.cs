
using GameHost.Features.SystemInfo.Web.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace GameHost.Features.SystemInfo.Web.Components;

public partial class SystemResourcesStatus
{
    [Parameter] public InfoPanelFormFactor FormFactor { get; set; } = InfoPanelFormFactor.Normal;

    public static string FormatMegabytes(float mb)
    {
        if (mb < 1024)
            return $"{mb:0.##} MB";

        float gb = mb / 1024;
        if (gb < 1024)
            return $"{gb:0.##} GB";

        float tb = gb / 1024;
        return $"{tb:0.##} TB";
    }
}
