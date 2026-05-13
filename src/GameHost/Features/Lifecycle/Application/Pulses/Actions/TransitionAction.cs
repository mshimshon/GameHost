using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

public sealed record TransitionAction : IAction
{
    public ServerInfoEntity? ServerInfo { get; set; }
}
