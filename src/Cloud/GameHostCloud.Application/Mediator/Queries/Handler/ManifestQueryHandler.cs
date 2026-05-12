using CoreMap;
using GameHostCloud.Application.Contracts.Responses;
using GameHostCloud.Domain.Entities.Enums;
using GameHostCloud.Domain.Respositories;
using GameHostCloud.Domain.ValueObjects;

namespace GameHostCloud.Application.Mediator.Queries.Handler;

internal class ManifestQueryHandler
{
    public async Task<ManifestResponse> Handle(GetManifestById query, ICoreMap coreMap, IManifestRepository manifestRepository, CancellationToken ct = default)
    {
        var manifestId = new ManifestId(query.Id);
        var result = await manifestRepository.GetManifestByIdAsync(manifestId, ct);
        var mappedResult = coreMap.Map(result).To<ManifestResponse>();
        return mappedResult;
    }

    public async Task<ICollection<ManifestResponse>> Handle(GetManifestsByDistro query, ICoreMap coreMap, IManifestRepository manifestRepository, CancellationToken ct = default)
    {
        Distro distro = (Distro)query.Distro;
        var result = await manifestRepository.GetManifestsByDistroAsync(distro, ct);
        var mappedResult = coreMap.MapEach(result).To<ManifestResponse>();
        return mappedResult;
    }

    public async Task<ICollection<ManifestResponse>> Handle(GetManifests query, ICoreMap coreMap, IManifestRepository manifestRepository, CancellationToken ct = default)
    {
        var result = await manifestRepository.GetManifestsAsync(ct);
        var mappedResult = coreMap.MapEach(result).To<ManifestResponse>();
        return mappedResult;
    }
}