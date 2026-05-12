using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.Events;

public sealed record ServerStateTransitionResponse
{
    public ServerTransition Transition { get; init; }
    public DateTime TransitionStartedAt { get; init; }
}
