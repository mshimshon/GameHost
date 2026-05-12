using CoreMap;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;

internal class ModListResponseToModListEntity : ICoreMapHandler<ModListResponse, ModListEntity>
{
    public ModListEntity Handler(ModListResponse data, ICoreMap alsoMap)
    {
        IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> mods =
            data.Mods.ToDictionary(
                p => new PartId(p.Key),
            p => (IReadOnlyList<ModEntity>)alsoMap.MapEach(p.Value).To<ModEntity>().ToList()
            .AsReadOnly()).AsReadOnly();

        return new ModListEntity(new ModListDescriptor(data.Id, data.Name), mods);
    }
}
