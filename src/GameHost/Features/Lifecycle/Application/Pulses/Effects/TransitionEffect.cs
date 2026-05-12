using CoreMap;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Payloads;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;
using GameHost.Features.Lifecycle.Domain.Enums;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal sealed class TransitionEffect : IEffect<TransitionAction>
{
    private readonly IStateAccessor<ServerState> _serverStatusStateAccess;
    private readonly IEventBus _eventBus;
    private readonly ICoreMap _coreMap;

    public TransitionEffect(IStateAccessor<ServerState> serverStatusStateAccess,
        IEventBus eventBus, ICoreMap coreMap)
    {
        _serverStatusStateAccess = serverStatusStateAccess;
        _eventBus = eventBus;
        _coreMap = coreMap;
    }
    public async Task EffectAsync(TransitionAction action, IDispatcher dispatcher)
    {
        var serverInfoState = _serverStatusStateAccess.State;
        if (action.ServerInfo != default && serverInfoState.Transition != ServerTransition.Idle)
        {
            bool isStartingTransitionCompleted = serverInfoState.Transition == ServerTransition.Starting && action.ServerInfo.Status == Status.Running;
            bool isStoppedTransitionCompleted = serverInfoState.Transition == ServerTransition.Stopping && action.ServerInfo.Status == Status.Stopped;
            //TODO: Settable in Settings
            var timeout = serverInfoState.TransitionStartedAt.AddMinutes(1).AddSeconds(30);
            bool isTransitionCompleted = isStartingTransitionCompleted || isStoppedTransitionCompleted;
            bool shouldWeTimeout = DateTime.UtcNow >= timeout;
            bool generatePayload = isTransitionCompleted || shouldWeTimeout;
            ServerStateTransitionPayload? payload = default;
            if (generatePayload)
            {
                payload = _coreMap.Map(serverInfoState).To<ServerStateTransitionPayload>();
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
