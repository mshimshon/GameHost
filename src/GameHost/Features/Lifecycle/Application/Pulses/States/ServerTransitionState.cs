using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.States;

public sealed record ServerTransitionState : IStateFeature
{
    public int HowManyCares { get; init; }
    public bool AmInstigator { get; init; }

}
