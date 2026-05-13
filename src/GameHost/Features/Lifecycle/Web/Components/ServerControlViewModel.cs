using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo.Enums;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Web.Components.ViewModels;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Tools;
using LunaticPanel.Core.Abstraction.Widgets;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Web.Components;


internal class ServerControlViewModel : WidgetViewModelBase, IServerControlViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly ICrazyReport _crazyReport;
    private readonly IStateAccessor<ServerState> _stateAccessor;
    private readonly IPanelControl _panelControl;

    public ServerState ServerState => _statePulse.StateOf<ServerState>(() => this, UpdateState);
    //public ServerState ServerState => _stateAccessor.State;
    public SystemInfoState SystemInfoState => _statePulse.StateOf<SystemInfoState>(() => this, UpdateState);
    public GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, UpdateParentChanges);
    public ServerTransitionState TransitionState => _statePulse.StateOf<ServerTransitionState>(() => this, UpdateState);

    public GameInfoEntity? GameInfo => GameInfoState?.GameInfo;

    public ServerControlViewModel(IStatePulse statePulse, ICrazyReport crazyReport, IStateAccessor<ServerState> stateAccessor, IPanelControl panelControl)
    {
        _statePulse = statePulse;
        _crazyReport = crazyReport;
        _stateAccessor = stateAccessor;
        _panelControl = panelControl;
        //_stateAccessor.OnStateChangedNoDetails += (_, e) => { _ = UpdateState(); };
        _crazyReport.SetModule<ServerControlViewModel>(LifecycleKeys.MODULE_NAME);
    }


    public async Task UpdateState()
    {
        _crazyReport.ReportError("{0} Received an Update on Circuit", nameof(ServerState));
        await UpdateChanges();

    }

    public async Task Start()
    {
        IsLoading = true;
        await _statePulse.Dispatcher.Prepare<ServerStartAction>().DispatchAsync();
        IsLoading = false;
    }

    public async Task Stop()
    {
        IsLoading = true;
        await _statePulse.Dispatcher.Prepare<ServerStopAction>().DispatchAsync();
        IsLoading = false;
    }
    protected override bool GetStateLoadingStatus() => IsWaiting();
    public bool IsRunning() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == ServerStatus.Running;
    public bool IsStopped() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == ServerStatus.Stopped;
    public bool IsRestarting() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == ServerStatus.Running;
    public bool IsFailed() => ServerState.ServerInfo != default && ServerState.ServerInfo.Status == ServerStatus.Failed;
    public bool IsWaiting() => ServerState.ServerInfo == default || ServerState.ServerInfo.Status == ServerStatus.Unknown;
    public Guid GetPanelId() => _panelControl.Id;
    public async Task StartCaringAboutTransitionAsync()
    {
        await _statePulse.Dispatcher.Prepare<TransitionCareAddAction>().Await().DispatchAsync();

    }
    public async Task StopCaringAboutTransitionAsync()
    {
        await _statePulse.Dispatcher.Prepare<TransitionCareRemoveAction>().Await().DispatchAsync();
    }
}
