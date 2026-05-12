using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class TransitionDoneReducer : IReducer<ServerState, TransitionDoneAction>
{
    public ServerState Reduce(ServerState state, TransitionDoneAction action)
    {

        var nstate = state with { Delay = 8, Transition = ServerTransition.Idle };
        return nstate;
    }
}
