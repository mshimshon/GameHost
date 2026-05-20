using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.Events;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.Events.Mapping;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo.Enums;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal sealed class TransitionEffect : IEffect<TransitionAction>
{
    private readonly IStateAccessor<ServerState> _serverStatusStateAccess;
    private readonly IEventBus _eventBus;

    public TransitionEffect(IStateAccessor<ServerState> serverStatusStateAccess,
        IEventBus eventBus)
    {
        _serverStatusStateAccess = serverStatusStateAccess;
        _eventBus = eventBus;
    }
    public async Task EffectAsync(TransitionAction action, IDispatcher dispatcher)
    {
        var serverInfoState = _serverStatusStateAccess.State;
        if (action.ServerInfo != default && serverInfoState.Transition != ServerTransition.Idle)
        {
            bool isStartingTransitionCompleted = serverInfoState.Transition == ServerTransition.Starting && action.ServerInfo.Status == ServerStatus.Running;
            bool isStoppedTransitionCompleted = serverInfoState.Transition == ServerTransition.Stopping && action.ServerInfo.Status == ServerStatus.Stopped;
            //TODO: Settable in Settings
            var timeout = serverInfoState.TransitionStartedAt.AddMinutes(1).AddSeconds(30);
            bool isTransitionCompleted = isStartingTransitionCompleted || isStoppedTransitionCompleted;
            bool shouldWeTimeout = DateTime.UtcNow >= timeout;
            bool generatePayload = isTransitionCompleted || shouldWeTimeout;
            ServerStateTransitionResponse? payload = default;
            if (generatePayload)
            {
                payload = serverInfoState.MapToApplication();
            }
            if (isTransitionCompleted)
            {
                await dispatcher.Prepare<TransitionDoneAction>().Await().DispatchAsync();
                await _eventBus.PublishDataAsync(LifecycleKeys.Events.ServerControl.TRANSITION_COMPLETED, payload!);

            }
            else if (shouldWeTimeout)
            {
                await dispatcher.Prepare<TransitionDoneAction>().Await().DispatchAsync();
                await _eventBus.PublishDataAsync(LifecycleKeys.Events.ServerControl.TRANSITION_TIMEDOUT, payload!);

            }
        }
    }
}
