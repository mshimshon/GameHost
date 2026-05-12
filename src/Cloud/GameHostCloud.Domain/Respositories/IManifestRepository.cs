using GameHostCloud.Domain.Entities;
using GameHostCloud.Domain.Entities.Enums;
using GameHostCloud.Domain.ValueObjects;

namespace GameHostCloud.Domain.Respositories;

public interface IManifestRepository
{
    Task<ManifestEntity> GetManifestByIdAsync(ManifestId id, CancellationToken ct = default);
    Task<ICollection<ManifestEntity>> GetManifestsByDistroAsync(Distro distro, CancellationToken ct = default);
    Task<ICollection<ManifestEntity>> GetManifestsAsync(CancellationToken ct = default);
}
