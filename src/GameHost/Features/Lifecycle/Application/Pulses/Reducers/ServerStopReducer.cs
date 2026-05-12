using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class ServerStopReducer : IReducer<ServerState, ServerStopAction>
{
    public ServerState Reduce(ServerState state, ServerStopAction action)
    {
        return state with
        {
            Delay = 1,
            Transition = ServerTransition.Stopping,
            TransitionStartedAt = DateTime.UtcNow
        };
    }
}