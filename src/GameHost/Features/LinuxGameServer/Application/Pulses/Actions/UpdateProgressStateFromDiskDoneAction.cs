using GameHost.Features.LinuxGameServer.Application.Models;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateProgressStateFromDiskDoneAction : IAction
{
    public GameServerInstallProcessModel? ProgressState { get; set; }
}
