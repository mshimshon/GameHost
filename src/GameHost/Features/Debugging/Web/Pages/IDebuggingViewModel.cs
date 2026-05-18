using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Debugging.Web.Pages;

public interface IDebuggingViewModel : IWidgetViewModel
{
    Type? SelectedType { get; set; }
    List<Type> States { get; }
    Task ChangeSelection(Type toRead);
    string CurrentPrint { get; }
}
