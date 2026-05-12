using CoreMap;
using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Application.Contracts.Responses.Mapping;

internal class InstallationStateToGameServerInfoEntity : ICoreMapHandler<InstallationStateDto, GameServerInfoEntity>
{
    public GameServerInfoEntity Handler(InstallationStateDto data, ICoreMap alsoMap)
        => new GameServerInfoEntity()
        {
            Id = data.Id,
            InstallDate = data.InstallDate
        };
}
