using GameHost.Features.Lifecycle.Application.Pulses.States;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.Events.Mapping;

internal static class ServerStateMappingExt
{
    public static ServerStateTransitionResponse MapToApplication(this ServerState data)
        => new() { Transition = data.Transition, TransitionStartedAt = data.TransitionStartedAt };
}
