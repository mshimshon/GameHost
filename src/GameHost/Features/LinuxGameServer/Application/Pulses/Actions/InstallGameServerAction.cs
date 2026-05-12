using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record InstallGameServerAction : ISafeAction
{
    public GameManifestResponse GameManifest { get; set; } = default!;
}
