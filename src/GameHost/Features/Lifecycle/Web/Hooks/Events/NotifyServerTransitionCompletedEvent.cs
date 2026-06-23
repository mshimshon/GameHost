using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Abstraction.Plugin;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using StatePulse.Net;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.Events;

namespace GameHost.Features.Lifecycle.Web.Hooks.Events;

[EventBusKey(LifecycleKeys.Events.ServerControl.TRANSITION_COMPLETED, CrossCircuitReceiver = EventBusSpreadType.CrossCircuitAll)]
internal class NotifyServerTransitionCompletedEvent : IEventBusHandler
{
    private readonly IStateAccessor<ServerTransitionState> _serverTransitionStateAccess;
    private readonly ICrazyReport<NotifyServerTransitionCompletedEvent> _crazyReport;
    private readonly IDispatcher _dispatcher;
    private readonly INotificationService _notificationService;
    private readonly IPluginContext _pluginContext;

    public NotifyServerTransitionCompletedEvent(INotificationService notificationService, IPluginContext pluginContext,
        IStateAccessor<ServerTransitionState> serverTransitionStateAccess,
        ICrazyReport<NotifyServerTransitionCompletedEvent> crazyReport, IDispatcher dispatcher)
    {
        _notificationService = notificationService;
        _pluginContext = pluginContext;
        _serverTransitionStateAccess = serverTransitionStateAccess;
        _crazyReport = crazyReport;
        _dispatcher = dispatcher;
        _crazyReport.SetModule(LifecycleKeys.MODULE_NAME);
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        if (_pluginContext.IsMasterCircuit) return;
        var data = evt.GetData()!.GetDataAs<ServerStateTransitionResponse>()!;
        var transitionState = _serverTransitionStateAccess.State;
        if (transitionState.AmInstigator)
            await _dispatcher.Prepare<TransitionInstigatorUnSetMeAction>().DispatchAsync();

        bool noOneCares = transitionState.HowManyCares <= 0;
        bool instigatorIsMe = transitionState.AmInstigator;
        bool instigatorIsNotMe = !instigatorIsMe;
        bool skipMe = instigatorIsNotMe && noOneCares;
        if (skipMe) return;
        await _notificationService.NotifyAsync($"Server {data.Transition} has completed.", NotificationSeverity.Success); // TODO: LOCALIZE
    }
}
