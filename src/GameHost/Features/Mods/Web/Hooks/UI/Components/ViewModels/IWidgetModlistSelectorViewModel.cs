using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.States;

namespace GameHost.Features.Mods.Web.Hooks.UI.Components.ViewModels;

public interface IWidgetModlistSelectorViewModel : IWidgetViewModel
{
    public ModListState ModsListState { get; }
}
