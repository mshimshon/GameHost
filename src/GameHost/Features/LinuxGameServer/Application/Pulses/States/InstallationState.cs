using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.States;

public record InstallationState : IStateFeatureSingleton
{
    public bool IsInstallationCompleted => InstalledGameServer != default && InProgressInstallation == default;


    public GameServerInfoResponse? InstalledGameServer { get; init; }
    public bool IsInstalledGameDiskLoaded { get; init; }
    public GameServerInstallProgressResponse? InProgressInstallation { get; init; }
    public bool IsProgressDiskLoaded { get; init; }

    public IReadOnlyCollection<GameManifestResponse> AvailableGameServers { get; init; } = Array.Empty<GameManifestResponse>().AsReadOnly();
    public bool AvailableGameServersLoading { get; init; }
    public long BusVersion { get; init; } = long.MinValue;
}
