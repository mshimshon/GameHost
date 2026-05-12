namespace GameHost.Features.Mods.Domain.Entities;

public sealed record ModFeatureEntity
{
    public bool Modding { get; init; }
    public bool ManualModUpload { get; init; }
}
