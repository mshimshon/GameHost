namespace GameHost.Features.LinuxGameServer.Domain.ValueObjects;

public sealed record ManifestId
{
    public string Value { get; init; } = default!;
    public ManifestId(string value)
    {
        Value = value;

    }
}
