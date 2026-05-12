
using CoreMap;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;

internal class ModResponseToModEntity : ICoreMapHandler<ModResponse, ModEntity>
{
    // TODO: Check for Circular Dependencies Issues and Improve List Conversion
    public ModEntity Handler(ModResponse data, ICoreMap alsoMap)
            => new(new ModId(data.Id), alsoMap.MapEach(data.Dependencies?.ToList() ?? new List<ModResponse>()).To<ModEntity>().ToList().AsReadOnly());

}
