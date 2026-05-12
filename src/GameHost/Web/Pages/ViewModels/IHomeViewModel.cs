using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Web.Pages.ViewModels;

public interface IHomeViewModel : IWidgetViewModel
{
    bool IsGameInfoAvailable { get; }

    bool IsServerInstalled { get; }
    bool IsModSupported { get; }

}
