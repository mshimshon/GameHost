using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Domain.Entites;

public sealed record GameConfigInfoEntity
{
    IReadOnlyDictionary<ConfigInfoKey, GameConfigEntity> ConfigDefinitions { get; init; } = default!;
}
