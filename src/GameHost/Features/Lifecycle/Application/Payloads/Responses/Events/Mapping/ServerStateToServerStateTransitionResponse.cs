using CoreMap;
using GameHost.Features.Lifecycle.Application.Pulses.States;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.Events.Mapping;

internal sealed class ServerStateToServerStateTransitionResponse : ICoreMapHandler<ServerState, ServerStateTransitionResponse>
{
    public ServerStateTransitionResponse Handler(ServerState data, ICoreMap alsoMap)
        => new() { Transition = data.Transition, TransitionStartedAt = data.TransitionStartedAt };
}
