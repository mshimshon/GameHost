using GameHost.Features.Mods.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Mods.Web.Hooks.UI.Components.ViewModels;

public interface IWidgetModListWorkspaceViewModel : IWidgetViewModel
{
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    bool IsGameInfoCrashed { get; }

}
