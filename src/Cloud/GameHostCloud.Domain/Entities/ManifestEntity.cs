using GameHostCloud.Domain.Entities.Enums;
using GameHostCloud.Domain.Exceptions;
using GameHostCloud.Domain.ValueObjects;

namespace GameHostCloud.Domain.Entities;

public sealed record ManifestEntity
{
    public ManifestId ManifestIdentity { get; private set; } = default!;
    public InstallerId InstallerIdentity { get; private set; } = default!;
    public IReadOnlyCollection<Distro> CompatibleDistro { get; private set; } = default!;
    public ManifestMetadata Metadata { get; private set; } = default!;

    // PRimitive Validation Happens here
    private ManifestEntity(ManifestId manifestId, InstallerId installerId, ManifestMetadata metadata, ICollection<Distro> compatibleDistro, string? icon = default)
    {
        ManifestIdentity = manifestId;
        InstallerIdentity = installerId;
        Metadata = metadata;
        if (compatibleDistro is null)
            throw new CompatibleDistroRequiredException();
        CompatibleDistro = compatibleDistro.ToList().AsReadOnly();
    }

    public static Task<ManifestEntity> CreateAsync(ManifestId manifestId, InstallerId installerId, ManifestMetadata metadata, ICollection<Distro> compatibleDistro)
        => Task.FromResult(new ManifestEntity(manifestId, installerId, metadata, compatibleDistro));

    public static ManifestEntity FromPersistance(ManifestId manifestId, InstallerId installerId, ManifestMetadata metadata, ICollection<Distro> compatibleDistro)
        => new ManifestEntity(manifestId, installerId, metadata, compatibleDistro);
}
