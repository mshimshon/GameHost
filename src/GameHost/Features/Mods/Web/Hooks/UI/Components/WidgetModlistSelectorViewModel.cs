using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Web.Hooks.UI.Components.ViewModels;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Hooks.UI.Components;

internal class WidgetModlistSelectorViewModel : WidgetViewModelBase, IWidgetModlistSelectorViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListState ModsListState => _statePulse.StateOf<ModListState>(() => this, UpdateParentChanges);
    public WidgetModlistSelectorViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;

    }
}
