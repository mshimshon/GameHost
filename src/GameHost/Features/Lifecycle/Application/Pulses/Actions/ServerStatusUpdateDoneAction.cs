using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record ServerStatusUpdateDoneAction : IAction
{
    public ServerInfoResponse? ServerInfo { get; set; }
}
