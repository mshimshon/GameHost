namespace GameHost.Features.Lifecycle.Domain.Entites;

public sealed record GameConfigEntity
{
    public string DisplayName { get; init; } = default!;
    public IReadOnlyCollection<GameConfigParamaterEntity> Parameters { get; init; } = default!;

}
