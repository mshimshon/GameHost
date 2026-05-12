namespace GameHost.Features.LinuxGameServer.Web.Components;

public partial class SetupProcess
{
    private const string MISCONFIGURE = ""; // TODO: LOCALIZE CHECK ALSO OTHER
    private const string LOADING_GAME_MANIFESTS = "Fetching Available Servers..."; // TODO: LOCALIZE CHECK ALSO OTHER
    private const string INSTALL_COMPLETED = "The game server was successfully installed, best to refresh this page."; // TODO: LOCALIZE CHECK ALSO OTHER
    protected override void OnWidgetDispose()
    {
        Console.WriteLine("Widget Disposed");
    }
}
