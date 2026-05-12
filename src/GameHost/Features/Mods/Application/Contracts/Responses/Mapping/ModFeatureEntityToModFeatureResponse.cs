using CoreMap;
using GameHost.Features.Mods.Domain.Entities;

namespace GameHost.Features.Mods.Application.Contracts.Responses.Mapping;

internal class ModFeatureEntityToModFeatureResponse : ICoreMapHandler<ModFeatureEntity, ModFeatureResponse>
{
    public ModFeatureResponse Handler(ModFeatureEntity data, ICoreMap alsoMap)
        => new()
        {
            IsEnabled = data.Modding,
            IsManualDownload = data.ManualModUpload
        };
}
