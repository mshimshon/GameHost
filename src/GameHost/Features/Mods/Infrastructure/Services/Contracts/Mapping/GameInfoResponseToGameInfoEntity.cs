using CoreMap;
using GameHost.Features.Mods.Domain.Entities;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;

internal sealed class GameInfoResponseToGameInfoEntity : ICoreMapHandler<GameInfoResponse, ModFeatureEntity>
{
    public ModFeatureEntity Handler(GameInfoResponse data, ICoreMap alsoMap)
        => new ModFeatureEntity()
        {
            ManualModUpload = data.ManualModUpload,
            Modding = data.Modding
        };
}
