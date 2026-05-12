using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public record PopulateAvailableGamesForInstallDoneAction
    : IAction
{
    public ICollection<GameManifestResponse> GameManifests { get; set; } = default!;
}
