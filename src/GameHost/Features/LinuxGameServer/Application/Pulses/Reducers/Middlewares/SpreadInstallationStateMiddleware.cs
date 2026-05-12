using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers.Middlewares;

/// <summary>
/// Provides middleware for handling installation state changes within the reducer pipeline. Publishes installation
/// state change events to the event bus after the reducer processes an action.
/// </summary>
internal class SpreadInstallationStateMiddleware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;
    private readonly ICrazyReport _crazyReport;

    public SpreadInstallationStateMiddleware(IEventBus eventBus, ICrazyReport<SpreadInstallationStateMiddleware> crazyReport)
    {
        _eventBus = eventBus;
        _crazyReport = crazyReport;
        crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
    }

    public async Task AfterReducing(object state, object action)
    {
        if (state.GetType() == typeof(InstallationState))
        {
            _crazyReport.ReportInfo("Detected State {0} from action {1}", state.GetType().Name, action.GetType().Name);
            await _eventBus.PublishDataAsync(LinuxGameServerKeys.Events.ON_GAME_SERVER_INSTALL_STATE_CHANGED, state);

        }
    }

    public Task BeforeReducing(object state, object action) => Task.CompletedTask;


}