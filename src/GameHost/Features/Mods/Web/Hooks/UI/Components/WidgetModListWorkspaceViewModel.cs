using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Web.Hooks.UI.Components.ViewModels;
using LunaticPanel.Core.Abstraction.Widgets;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Hooks.UI.Components;

internal class WidgetModListWorkspaceViewModel : WidgetViewModelBase, IWidgetModListWorkspaceViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateChanges);
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    public Guid ModListId { get; set; }
    public bool IsGameInfoCrashed => !ModListState.IsFeatureInfoLoading && ModListState.FeatureInfo == default;
    public WidgetModListWorkspaceViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }


    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading || ModListState.IsFeatureInfoLoading;

}
