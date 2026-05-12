namespace GameHost.Features.LinuxGameServer.Domain.ValueObjects;

public sealed record ManifestId
{
    public string Id { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
    public ManifestId(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;

    }
}
