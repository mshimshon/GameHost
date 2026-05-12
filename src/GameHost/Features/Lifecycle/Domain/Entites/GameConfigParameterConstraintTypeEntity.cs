using GameHost.Features.Lifecycle.Domain.Enums;

namespace GameHost.Features.Lifecycle.Domain.Entites;

public sealed record GameConfigParameterConstraintTypeEntity
{
    public string Key { get; init; } = default!;
    public ConfigParameterConstraintType Constraint { get; init; }
    public string Message { get; init; } = default!;
}
