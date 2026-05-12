using CoreMap;
using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Application.Contracts.Responses.Mapping;

internal class GameManifestEntityToGameManifestResponse : ICoreMapHandler<GameManifestEntity, GameManifestResponse>
{
    public GameManifestResponse Handler(GameManifestEntity data, ICoreMap alsoMap)
        => new()
        {
            DisplayName = data.Id.DisplayName,
            Id = data.Id.Id,
            DistroCompatibility = data.DistroCompatibility.ToList(),
            Icon = data.Icon,
            InstallerSource = data.InstallerSource,
            InstallerName = data.InstallerName
        };
}
