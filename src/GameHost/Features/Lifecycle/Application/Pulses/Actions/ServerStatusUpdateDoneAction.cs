using GameHost.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record ServerStatusUpdateDoneAction : IAction
{
    public ServerInfoEntity? ServerInfo { get; set; }
}
