using GameHost.Features.LinuxGameServer.Application.Models;
using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Application.Services;

public interface ILinuxGameServerService
{
    Task<ICollection<GameManifestEntity>?> GetAvailableGames(CancellationToken ct = default);

    Task PerformServerInstallation(string id, string installerName, CancellationToken ct = default);

    Task<GameServerInstallProcessModel?> GetInstallationProgress(CancellationToken ct = default);
    Task<GameServerInfoEntity?> GetInstalledGameServer(CancellationToken ct = default);



}
