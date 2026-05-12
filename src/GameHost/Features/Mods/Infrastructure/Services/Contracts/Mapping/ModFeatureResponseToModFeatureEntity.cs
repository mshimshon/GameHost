using CoreMap;
using GameHost.Features.Mods.Domain.Entities;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;

internal class ModFeatureResponseToModFeatureEntity : ICoreMapHandler<ModFeatureResponse, ModFeatureEntity>
{
    public ModFeatureEntity Handler(ModFeatureResponse data, ICoreMap alsoMap)
        => new()
        {
            ManualModUpload = data.RequiredManualDownload
        };
}
