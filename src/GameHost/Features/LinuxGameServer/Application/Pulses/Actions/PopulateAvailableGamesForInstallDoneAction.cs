using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record PopulateAvailableGamesForInstallDoneAction
    : IAction
{
    public ICollection<GameManifestResponse> GameManifests { get; set; } = default!;
}
