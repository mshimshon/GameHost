using CoreMap;
using GameHost.Features.Mods.Domain.Entities;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;

internal class ModListEntityToModListResponse : ICoreMapHandler<ModListEntity, ModListResponse>
{
    //  TODO : Improve Dictionary Conversion
    public ModListResponse Handler(ModListEntity data, ICoreMap alsoMap)
    {
        var mods =
    data.Mods.ToDictionary(p => p.Key.Id, p => alsoMap.MapEach(p.Value.ToList()).To<ModResponse>().ToList());
        return new()
        {
            Id = data.Descriptor.Id,
            Name = data.Descriptor.Name,
            Mods = mods
        };
    }
}
