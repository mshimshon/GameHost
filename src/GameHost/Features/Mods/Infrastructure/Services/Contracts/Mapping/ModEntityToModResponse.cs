using CoreMap;
using GameHost.Features.Mods.Domain.Entities;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;

internal class ModEntityToModResponse : ICoreMapHandler<ModEntity, ModResponse>
{
    // TODO: Check for Circular Dependencies and Improve Efficiency Mapping the List
    public ModResponse Handler(ModEntity data, ICoreMap alsoMap)
        => new()
        {
            Id = data.Id.Id,
            Name = data.Name?.Name,
            Dependencies = alsoMap.MapEach(data.Dependencies?.ToList() ?? new List<ModEntity>()).To<ModResponse>().ToList()
        };
}
