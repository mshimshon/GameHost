using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record InstallGameServerAction : ISafeAction
{
    public GameManifestResponse GameManifest { get; set; } = default!;
}
