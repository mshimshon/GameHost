using GameHost.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record GameServerInstalledAction : IAction
{
    public GameServerInfoEntity GameServerInstalled { get; set; } = default!;
}
