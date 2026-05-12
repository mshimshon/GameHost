using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Domain.Entites;

public record GameConfigParamaterEntity
{
    // Key Value Object includes ENum of Type p
    public ConfigParameter Key { get; init; } = default!;
    public string Label { get; init; } = default!;
    public string Description { get; init; } = default!;
    public bool Required { get; init; }
    public bool Editable { get; init; }
    public string? DefaultValue { get; init; }
    public string Category { get; init; } = default!;
    public string? Warning { get; init; }
    public GameConfigParameterValidationEntity? Validation { get; init; }
    public GameConfigParameterConstraintTypeEntity? RelatedTo { get; init; }
}
