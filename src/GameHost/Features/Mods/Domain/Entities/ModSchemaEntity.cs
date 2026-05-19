namespace GameHost.Features.Mods.Domain.Entities;

public sealed record ModSchemaEntity
{
    public IReadOnlyList<ModSchemaPartEntity> Parts { get; }
    public ModSchemaEntity(IReadOnlyList<ModSchemaPartEntity> parts)
    {
        Parts = parts.ToList().AsReadOnly();
    }
}
