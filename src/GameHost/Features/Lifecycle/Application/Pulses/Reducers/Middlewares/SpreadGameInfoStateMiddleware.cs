using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers.Middlewares;

internal class SpreadGameInfoStateMiddleware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;
    private readonly ICrazyReport _crazyReport;

    public SpreadGameInfoStateMiddleware(IEventBus eventBus, ICrazyReport<SpreadGameInfoStateMiddleware> crazyReport)
    {
        _eventBus = eventBus;
        _crazyReport = crazyReport;
        crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
    }

    public async Task AfterReducing(object state, object action)
    {
        //TODO: COntiNUE WHERE WE LEFT OFF
        /*
            We implementating the ConfigInfo file from the game which contains information and config file that could edited the same way the startup params.
            The goal is implement at least the config info schematic into state for UI, check if we can reuse start parameter UI for a more universal system.
            After that we must stop and review the code structure and bring that code compliant with Clean Architecture methodologie.
         */
        if (state.GetType() == typeof(GameInfoState))
        {
            _crazyReport.ReportInfo("Detected Change for State {0} with Action {1}", state.GetType(), action.GetType());
            await _eventBus.PublishDataAsync(LifecycleKeys.Events.GAMEINFO_STATE_CHANGED, state);

        }
    }

    public Task BeforeReducing(object state, object action) => Task.CompletedTask;
}
