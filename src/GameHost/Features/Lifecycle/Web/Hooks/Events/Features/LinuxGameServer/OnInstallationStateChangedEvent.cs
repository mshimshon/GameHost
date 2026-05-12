using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Web.Hooks.Events.Features.LinuxGameServer.Dto;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Web.Hooks.Events.Features.LinuxGameServer;

[EventBusId(LinuxGameServerKeys.Events.ON_GAME_SERVER_INSTALL_STATE_CHANGED)]
internal class OnInstallationStateChangedEvent : IEventBusHandler
{
    /*
     * IScheduledEventBusHandler
     * Receiver Receives -> Event -> Fetch All Match Id if cross circuit resolve for all circuits call 
     * Check if type Implement IEventBusHandler.HandleAsync or IScheduledEventBusHandler
     */
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<GameInfoState> _gameInfoStateAccess;
    private readonly ICrazyReport _crazyReport;

    public OnInstallationStateChangedEvent(IDispatcher dispatcher, IStateAccessor<GameInfoState> gameInfoStateAccess, ICrazyReport crazyReport)
    {
        _dispatcher = dispatcher;
        _gameInfoStateAccess = gameInfoStateAccess;
        _crazyReport = crazyReport;
        crazyReport.SetModule<OnInstallationStateChangedEvent>(LifecycleKeys.MODULE_NAME);
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        _crazyReport.ReportInfo("Incoming Event from {0}", LinuxGameServerKeys.Events.ON_GAME_SERVER_INSTALL_STATE_CHANGED);
        _crazyReport.ReportInfo("GameInfo is {0}", _gameInfoStateAccess.State.GameInfo?.ToString() ?? "null");
        if (_gameInfoStateAccess.State.GameInfo != default) return;
        var state = await evt.ReadAs<InstallationStateResponse>();
        _crazyReport.ReportInfo("InstallationStateResponse is {0}", state.ToString());
        if (state.IsInstallationCompleted)
            await _dispatcher.Prepare<ServerGameInfoUpdateAction>().DispatchAsync();
    }
}
