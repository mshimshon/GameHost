using GameHost.Features.LinuxGameServer.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateInstalledGameServerDoneAction : IAction
{
    public GameServerInfoEntity? GameServerInfo { get; set; }
}
