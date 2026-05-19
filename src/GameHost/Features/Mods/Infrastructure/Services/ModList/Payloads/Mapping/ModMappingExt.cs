using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads.Mapping;

internal static class ModMappingExt
{
    public static ModEntity MapToDomain(this ModResponse data)
        => new(new(data.Id), data.Dependencies?.Select(p => p.MapToDomain()).ToList().AsReadOnly())
        {
            Name = data.Name != default ? new(data.Name) : default,
        };
    public static ModFeatureEntity MapToDomain(this ModFeatureResponse data)
        => new()
        {
            ManualModDownload = data.ManualModDownload,
            Modding = data.Modding
        };

    private static IReadOnlyList<ModSchemaPartEntity> MapToDomain(this Dictionary<string, ModSchemaPartResponse> data)
        => data.Select(p => new ModSchemaPartEntity(p.Key, p.Value.Name, p.Value.Type)).ToList().AsReadOnly();
    public static ModSchemaEntity MapToDomain(this ModSchemaResponse data)
    => new(data.ModSchema.MapToDomain());


    private static IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> MapToDomain(this Dictionary<string, List<ModResponse>> data)
    {
        return data.ToDictionary(
        p => new PartId(p.Key),
        p => (IReadOnlyList<ModEntity>)p.Value.Select(x => x.MapToDomain()).ToList().AsReadOnly())
        .AsReadOnly();
    }

    public static ModListEntity MapToDomain(this ModListResponse data)
    => new(new(data.Id, data.Name), data.Mods.MapToDomain());

    private static Dictionary<string, List<ModResponse>> MapToInfrastructure(this IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> data)
    {
        return data.ToDictionary(
        p => p.Key.Id,
        p => p.Value.Select(x => x.MapToInfrastructure()).ToList());
    }
    public static ModListResponse MapToInfrastructure(this ModListEntity data)
        => new()
        {
            Mods = data.Mods.MapToInfrastructure(),
            Id = data.Descriptor.Id,
            Name = data.Descriptor.Name
        };

    public static ModResponse MapToInfrastructure(this ModEntity data)
    => new()
    {
        Id = data.Id.Id,
        Dependencies = data.Dependencies?.Select(p => p.MapToInfrastructure()).ToList(),
        Name = data.Name?.Name,
    };

    public static ModFeatureResponse MapToInfrastructure(this ModFeatureEntity data)
        => new()
        {
            ManualModDownload = data.ManualModDownload,
            Modding = data.Modding
        };

}
