using CoreMap;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Mapping;

public class GameInfoResponseToGameInfoEntity : ICoreMapHandler<GameInfoResponse, GameInfoEntity>
{
    public GameInfoEntity Handler(GameInfoResponse data, ICoreMap alsoMap) => new GameInfoEntity()
    {
        ManualModUpload = data.ManualModUpload,
        Modding = data.Modding,
        Name = data.Name,
        SteamInfo = data.Steam && !string.IsNullOrWhiteSpace(data.SteamAppId) ? new SteamGameId(data.SteamAppId, data.Modding && data.Workshop) : default,
        StartupParameters = alsoMap.MapEach(data.Parameters).To<GameConfigParamaterEntity>()
    };
}
