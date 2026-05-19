using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateProgressStateFromDiskDoneAction : IAction
{
    public GameServerInstallProgressResponse? ProgressState { get; set; }
}
