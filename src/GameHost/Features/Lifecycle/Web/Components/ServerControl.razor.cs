namespace GameHost.Features.Lifecycle.Web.Components;

public partial class ServerControl
{
    // TODO: CHECK STEAM CHIPS COMMENT IN RAZOR PAGE OF THIS
    // TODO: QUUERY ENGINE BUS TO SHOW CHIPS
    private const string AWAITING_FOR_SERVER_INFO = "Awaiting for Server Info..."; // TODO: Localize
    private const string SERVER_NOT_INSTALLED = "The server is misconfigured, it seems like the game server file has not been properly setup, contact admin or support."; // TODO: LOCALIZE CHECK ALSO OTHER

    protected override async Task OnWidgetDisposeAsync()
    {
        await ViewModel.StopCaringAboutTransitionAsync();
    }

    protected override async Task OnWidgetInitializedAsync()
    {
        await ViewModel.StartCaringAboutTransitionAsync();

    }
}
