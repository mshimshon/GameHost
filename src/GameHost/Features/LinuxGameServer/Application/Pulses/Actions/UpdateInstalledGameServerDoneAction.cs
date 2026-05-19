using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateInstalledGameServerDoneAction : IAction
{
    public GameServerInfoResponse? GameServerInfo { get; set; }
}
