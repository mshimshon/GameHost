using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Domain.Entites;

public sealed record GameInfoEntity
{
    public string Name { get; init; } = default!;
    public bool IsSteam => SteamInfo != default;
    public SteamGameId? SteamInfo { get; init; }
    // TODO: Remove only include for Mod Module.
    public bool Modding { get; init; }
    public bool ManualModUpload { get; init; }
    public ICollection<GameConfigParamaterEntity>? StartupParameters { get; init; }
}
