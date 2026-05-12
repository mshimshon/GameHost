using GameHostCloud.Domain.Entities;
using GameHostCloud.Domain.Entities.Enums;

namespace GameHostCloud.Domain.Respositories;

public interface IDistroDependencyRepository
{
    Task<DistroDependenciesEntity> GetDistroDependenciesAsync(Distro distro, CancellationToken ct = default);

}
