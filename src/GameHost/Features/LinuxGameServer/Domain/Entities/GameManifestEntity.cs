using GameHost.Features.LinuxGameServer.Domain.ValueObjects;

namespace GameHost.Features.LinuxGameServer.Domain.Entities;

public sealed record GameManifestEntity
{
    public ManifestId Id { get; init; } = default!;
    public string? Icon { get; init; }
    public IReadOnlyCollection<string> DistroCompatibility { get; init; } = default!;
    public string InstallerName { get; init; } = default!;
    public string InstallerSource { get; init; } = default!;
}
