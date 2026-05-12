using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Mods.Web.Pages.ViewModels;

public interface IModListHomeViewModel : IWidgetViewModel
{
    Guid InitialId { get; set; }
    Task GetAsync(Guid id);
}
