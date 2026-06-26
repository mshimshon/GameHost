using GameHost.Core;
using GameHost.Core.Features;
using Microsoft.AspNetCore.Components;

namespace GameHost.Features.Mods.Web.Hooks.UI.Components;

public partial class WidgetModListWorkspace
{
    private const string SERVER_NOT_INSTALLED = "The server is misconfigured, it seems like the game server file has not been properly setup, contact admin or support."; // TODO: LOCALIZE CHECK ALSO OTHER

    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Parameter] public bool AutoRedirect { get; set; } = false;
    [Parameter] public string LinkBaseNoTrailing { get; set; } = $"/{GameHostKeys.ASSEMBLY_NAME}/{ModListKeys.MODULE_NAME}";
    protected override async Task OnWidgetParametersSetAsync()
    {
    }

}
