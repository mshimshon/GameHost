using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal sealed class TransitionCareAddReducer : IReducer<ServerTransitionState, TransitionCareAddAction>
{
    public ServerTransitionState Reduce(ServerTransitionState state, TransitionCareAddAction action)
    {
        Console.WriteLine($"CARING COUNT: {state.HowManyCares + 1}");
        return state with { HowManyCares = state.HowManyCares + 1 };
    }
}
