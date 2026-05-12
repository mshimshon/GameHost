using CoreMap;
using GameHostCloud.Application.Contracts.Responses;
using GameHostCloud.Domain.Entities.Enums;
using GameHostCloud.Domain.Respositories;

namespace GameHostCloud.Application.Mediator.Queries.Handler;

internal class DependenciesQueryHandler
{
    public async Task<DistroDependenciesResponse> Handle(GetDistroDependencies query, ICoreMap coreMap, IDistroDependencyRepository repo, CancellationToken ct = default)
    {
        Distro distro = (Distro)query.Distro;
        var result = await repo.GetDistroDependenciesAsync(distro, ct);
        var mappedResult = coreMap.Map(result).To<DistroDependenciesResponse>();
        return mappedResult;
    }

}
