using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Web.Pages.Hooks.UI.Components.ViewModels;

public interface IWidgetMainPageMenuLinkViewModel : IWidgetViewModel
{
    bool IsGameInfoAvailable { get; }
    bool IsServerInstalled { get; }
    bool DoesSupportMods { get; }
    bool RequiresManualModUpload { get; }
}
