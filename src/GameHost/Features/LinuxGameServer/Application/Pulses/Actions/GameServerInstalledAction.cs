using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record GameServerInstalledAction : IAction
{
    public GameServerInfoResponse GameServerInstalled { get; set; } = default!;
}
