using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;

namespace GameHost.Features.Lifecycle.Application.Payloads;

public sealed record ServerStateTransitionPayload
{
    public ServerTransition Transition { get; init; }
    public DateTime TransitionStartedAt { get; init; }
}
