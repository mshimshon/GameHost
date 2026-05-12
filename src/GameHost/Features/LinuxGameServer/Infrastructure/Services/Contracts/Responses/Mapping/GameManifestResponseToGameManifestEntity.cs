using CoreMap;
using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Contracts.Responses.Mapping;

internal class GameManifestResponseToGameManifestEntity : ICoreMapHandler<GameManifestResponse, GameManifestEntity>
{
    public GameManifestEntity Handler(GameManifestResponse data, ICoreMap alsoMap)
        => new GameManifestEntity()
        {
            DistroCompatibility = data.CompatibleDistro?.ToList()?.AsReadOnly() ?? new List<string>().AsReadOnly(),
            Icon = data.Icon,
            Id = new(data.Id, data.DisplayName),
            InstallerSource = data.InstallerSource,
            InstallerName = data.InstallerName
        };
}
