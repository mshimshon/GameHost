using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Messaging.Common;
using LunaticPanel.Core.Abstraction.Messaging.EventScheduledBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Web.Hooks.Events.Scheduled;

[EventScheduledBusKey(LifecycleKeys.Events.Scheduled.GAME_SERVER_INFO_CHECK, 0, 4,
    RunAtStartup = true,
    ScheduleAtStartup = true,
    ServiceLifetime = EBusLifetime.Transient)]
internal class RefreshServerStatusInfoEvent : IEventScheduledBusHandler
{
    private readonly IStateAccessor<GameInfoState> _gameInfoStateAccess;
    private readonly ICrazyReport _crazyReport;
    private readonly IDispatcher _dispatcher;
    private static bool _isBusy;
    private static readonly object _lock = new();
    public RefreshServerStatusInfoEvent(IStateAccessor<GameInfoState> gameInfoStateAccess,
        ICrazyReport crazyReport,
        IDispatcher dispatcher)
    {
        _gameInfoStateAccess = gameInfoStateAccess;
        _crazyReport = crazyReport;
        _dispatcher = dispatcher;
        _crazyReport.SetModule<RefreshServerStatusInfoEvent>(LifecycleKeys.MODULE_NAME);
    }
    public EventScheduledBusMessageData DueToExecute(IEventScheduledBusMessage msg, CancellationToken ct = default)
    {
        if (_gameInfoStateAccess.State.GameInfo == default)
            return msg.ReplyWithAction(Execute).SkipExecution()
                .NextSchedule(new TimeSpan(0, 0, 15));

        lock (_lock)
        {
            if (_isBusy)
                return msg.ReplyWithAction(Execute).SkipExecution()
                    .NextSchedule(new TimeSpan(0, 0, 1));
            _isBusy = true;
        }
        return msg.ReplyWithAction(Execute);
    }

    private async Task Execute(CancellationToken ct = default)
    {
        try
        {
            await _dispatcher.Prepare<ServerStatusUpdateAction>().Await().DispatchAsync();
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);
        }
        finally
        {
            lock (_lock)
                _isBusy = false;
        }

    }
}
