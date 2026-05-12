using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Domain.Entities;

public sealed record ModEntity
{
    public ModId Id { get; }
    public ModName? Name { get; init; }
    private List<ModEntity> _dependencies;
    public IReadOnlyList<ModEntity>? Dependencies => _dependencies.AsReadOnly();
    public ModEntity(ModId id, IReadOnlyList<ModEntity>? dependencies = default)
    {
        Id = id;
        _dependencies = dependencies?.ToList() ?? new();
    }
}
