using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal sealed class TransitionCareRemoveReducer : IReducer<ServerTransitionState, TransitionCareRemoveAction>
{
    public ServerTransitionState Reduce(ServerTransitionState state, TransitionCareRemoveAction action)
    {
        int nextCareCount = state.HowManyCares - 1;
        Console.WriteLine($"CARING COUNT: {nextCareCount}");
        if (nextCareCount < 0) nextCareCount = 0;

        return state with { HowManyCares = nextCareCount };
    }
}
