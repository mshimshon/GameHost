using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Domain.Entities;

public sealed record ModSchemaPartEntity
{
    public PartId Id { get; }
    public PartName Name { get; }
    public PartType Type { get; }
    public ModSchemaPartEntity(string id, string name, string type)
    {
        Id = new(id);
        Name = new(name);
        Type = new(type);
    }
}
