using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

public sealed record TransitionAction : IAction
{
    public ServerInfoResponse? ServerInfo { get; set; }
}
