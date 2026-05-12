using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Domain.Entities;

public sealed record ModListEntity
{

    public ModListDescriptor Descriptor { get; }
    private readonly Dictionary<PartId, List<ModEntity>> _mods;
    public IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> Mods { get; }
    public ModListEntity(ModListDescriptor descriptor, IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> mods)
    {
        _mods = mods.ToDictionary(p => p.Key, p => p.Value.ToList());
        Mods = _mods.ToDictionary(p => p.Key, p => (IReadOnlyList<ModEntity>)p.Value.AsReadOnly()).AsReadOnly();
        Descriptor = descriptor;
    }
}