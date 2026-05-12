using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record GameServerInstallFailedAction : IAction
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string FailureReason { get; set; } = default!;
}
