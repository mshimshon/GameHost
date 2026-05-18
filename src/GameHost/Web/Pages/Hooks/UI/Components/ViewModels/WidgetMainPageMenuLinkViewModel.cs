using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using GameHost.Features.Mods.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;
using StatePulse.Net;

namespace GameHost.Web.Pages.Hooks.UI.Components.ViewModels;

internal class WidgetMainPageMenuLinkViewModel : WidgetViewModelBase, IWidgetMainPageMenuLinkViewModel
{
    private readonly IStatePulse _statePulse;
    public InstallationState InstallationState => _statePulse.StateOf<InstallationState>(() => this, UpdateChanges);
    public GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, UpdateChanges);
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);

    public bool IsGameInfoAvailable => GameInfoState.GameInfo != default;

    public bool IsServerInstalled => InstallationState.IsInstallationCompleted;

    public bool DoesSupportMods => ModListState.FeatureInfo?.IsEnabled ?? false;

    public bool RequiresManualModUpload => ModListState.FeatureInfo?.IsManualDownload ?? false;

    // TODO: USE CONFIG TO ENABLE/DISABLE DEBUG MODE
#if DEBUG
    public bool DebugEnabled => true;

#else
    public bool DebugEnabled => false;

#endif
    public WidgetMainPageMenuLinkViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }
    protected override bool GetStateLoadingStatus() => InstallationState.InProgressInstallation != default;
}
