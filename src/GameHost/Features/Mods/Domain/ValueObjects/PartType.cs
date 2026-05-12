using GameHost.Features.Mods.Domain.Exceptions.Extensions;

namespace GameHost.Features.Mods.Domain.ValueObjects;

public sealed record PartType
{
    public string Type { get; }
    public PartType(string type)
    {
        Type = type;
        Type.ThrowIfNullOrWhiteSpace<PartType>(nameof(Type));
    }
}
