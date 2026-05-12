using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.States;

namespace GameHost.Features.Mods.Web.Components.ViewModels;

public interface IModListExplorerViewModel : IWidgetViewModel
{
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    Task GetAsync(Guid id);
    Task GetAvailableAsync();
}
