using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Web.Components.ViewModels;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Components;

internal sealed class ModListSelectorViewModel : WidgetViewModelBase, IModListSelectorViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly ICrazyReport _crazyReport;

    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    public Guid CurrentModList { get; set; }
    public Guid InitialValue { get; private set; }
    public ModListSelectorViewModel(IStatePulse statePulse, ICrazyReport crazyReport)
    {
        _statePulse = statePulse;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<ModListSelectorViewModel>(ModListKeys.MODULE_NAME);

    }

    protected override void OnViewModelInitialized()
    {
        InitialValue = ModListState.Active?.Id ?? Guid.Empty;
        CurrentModList = InitialValue;
    }

    protected override void OnViewModelBeforeRender()
    {
        if (IsStateOutOfSync())
        {
            InitialValue = ModListState.Active?.Id ?? Guid.Empty;
            CurrentModList = InitialValue;
        }
    }

    private bool IsStateOutOfSync()
    {
        bool areBothDefined = InitialValue != Guid.Empty && ModListState.Active != default;
        bool wasCurrentSetWhenInitialDef = InitialValue == Guid.Empty && ModListState.Active != default;
        bool wasInitialSetWhenCurrentDef = InitialValue != Guid.Empty && ModListState.Active == default;
        bool bothdefinedButDifferent = areBothDefined && InitialValue != ModListState.Active!.Id;
        bool requiresUpdate = wasCurrentSetWhenInitialDef || wasInitialSetWhenCurrentDef || bothdefinedButDifferent;
        return requiresUpdate;
    }


    public async Task SaveAsync()
    {
        var newElement = CurrentModList != default ? ModListState.Available.SingleOrDefault(p => p.Id == CurrentModList) : default;
        await _statePulse.Dispatcher
            .Prepare<UpdateCurrentModListAction>()
            .With(p => p.Current, newElement)
            .DispatchAsync();
    }
}
