using CoreMap;
using GameHost.Features.Lifecycle.Application.Pulses.States;

namespace GameHost.Features.Lifecycle.Application.Payloads.Mapping;

internal sealed class ServerStateToServerStateTransitionPayload : ICoreMapHandler<ServerState, ServerStateTransitionPayload>
{
    public ServerStateTransitionPayload Handler(ServerState data, ICoreMap alsoMap)
        => new() { Transition = data.Transition, TransitionStartedAt = data.TransitionStartedAt };
}
