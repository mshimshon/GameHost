using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Web.Components.ViewModels;

public class SetupProcessViewModel : WidgetViewModelBase, ISetupProcessViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;
    public GameManifestResponse KeyGame { get; set; } = default!;

    public InstallationState InstallState => _statePulse.StateOf<InstallationState>(() => this, UpdateState);
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    private bool _isInstallCompleted;
    public async Task UpdateState()
    {

        //Console.WriteLine($"State updated: {(InstallState.ToString())}");
        LastUpdate = DateTime.UtcNow;
        if (_isInstallCompleted != InstallState.IsInstallationCompleted)
        {
            _isInstallCompleted = InstallState.IsInstallationCompleted;
            await UpdateParentChanges();
        }
        else
            await UpdateChanges();
        //await Task.Delay(10000);
        //await UpdateChanges();
    }
    public SetupProcessViewModel(IStatePulse statePulse, ICrazyReport<SetupProcessViewModel> crazyReport)
    {
        _statePulse = statePulse;
        _dispatcher = statePulse.Dispatcher;
        crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
        crazyReport.ReportInfo("Loaded Widget {0} and Found {1} Games Available.", nameof(SetupProcessViewModel), InstallState.AvailableGameServers?.Count ?? 0);
        if (InstallState.AvailableGameServers != default && InstallState.AvailableGameServers.Count > 0)
            KeyGame = InstallState.AvailableGameServers.First();
    }

    public async Task InstallAsync()
    {
        IsLoading = true;
        await _dispatcher.Prepare<InstallGameServerAction>()
            .With(p => p.GameManifest, KeyGame)
            .DispatchAsync();
        IsLoading = false;
    }
    protected override bool GetStateLoadingStatus() => !InstallState.IsInstalledGameDiskLoaded || !InstallState.IsProgressDiskLoaded;

}
