using GameHost.Core.Features;
using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Infrastructure.Configurations;
using LunaticPanel.Core.Abstraction.Messaging.EventScheduledBus;
using LunaticPanel.Core.Abstraction.Tools.EventScheduler;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Web.Hooks.Events.Scheduled;

[EventScheduledBusId(SystemInfoKeys.Events.UpdateInformation, 0, 10, RunAtStartup = true)]
internal sealed class PeriodicSystemInfoUpdate : IEventScheduledBusHandler
{
    private readonly IDispatcher _dispatcher;
    private readonly LinuxSystemInfoConfiguration _linuxSystemInfoConfiguration;
    private readonly IEventScheduler _eventScheduler;
    private readonly ICrazyReport _crazyReport;

    public PeriodicSystemInfoUpdate(IDispatcher dispatcher, LinuxSystemInfoConfiguration linuxSystemInfoConfiguration, IEventScheduler eventScheduler, ICrazyReport crazyReport)
    {
        _dispatcher = dispatcher;
        _linuxSystemInfoConfiguration = linuxSystemInfoConfiguration;
        _eventScheduler = eventScheduler;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<PeriodicSystemInfoUpdate>(SystemInfoKeys.MODULE_NAME);
    }

    public EventScheduledBusMessageData DueToExecute(IEventScheduledBusMessage msg, CancellationToken ct = default)
    {
        return msg.ReplyWithAction(UpdateCycle)
            .NextTiming(new TimeSpan(0, 0, _linuxSystemInfoConfiguration.PeriodicResourceCheckDelaySeconds));
    }


    private async Task UpdateCycle(CancellationToken ct = default)
    {
        // TODO: Handle Exceptions
        await _dispatcher.Prepare<SystemInfoUpdateAction>().Await().DispatchAsync();

    }
}
/*
 * IScheduleRunner, IScheduleAction<TAction>, IScheduledStatePulseAction<TAction> TAction = IAction or class
 * ActionSchedule Record -> Datetime nextRun
 * IScheduledAction Task<ActionSchedule> Run(Ct); 
 * [ScheduledActionId("ID", InitialTick, RunOnceAtStartup)] =>  
 */