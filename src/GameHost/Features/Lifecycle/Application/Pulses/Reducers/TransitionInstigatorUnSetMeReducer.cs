using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class TransitionInstigatorUnSetMeReducer : IReducer<ServerTransitionState, TransitionInstigatorUnSetMeAction>
{
    public ServerTransitionState Reduce(ServerTransitionState state, TransitionInstigatorUnSetMeAction action)
        => state with { AmInstigator = false };
}
