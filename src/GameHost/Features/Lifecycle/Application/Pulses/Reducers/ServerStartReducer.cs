using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class ServerStartReducer : IReducer<ServerState, ServerStartAction>
{

    public ServerState Reduce(ServerState state, ServerStartAction action)
        => state with
        {
            Delay = 1,
            Transition = States.Enums.ServerTransition.Starting,
            TransitionStartedAt = DateTime.UtcNow
        };
}
