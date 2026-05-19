using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Application.Payloads.Responses.Mapping;

internal static class ModMappingExt
{
    public static ModFeatureResponse MapToApplication(this ModFeatureEntity data)
        => new()
        {
            IsEnabled = data.Modding,
            IsManualDownload = data.ManualModDownload
        };


    public static ModResponse MapToApplication(this ModEntity data)
    => new()
    {
        Id = data.Id.Id,
        Name = data.Name?.Name,
        Dependencies = data.Dependencies?.Select(p => p.MapToApplication()).ToList()
    };

    public static ModEntity MapToDomain(this ModResponse data)
        => new ModEntity(new(data.Id), data.Dependencies?.Select(p => p.MapToDomain())?.ToList().AsReadOnly())
        {
            Name = data.Name != default ? new ModName(data.Name) : default,
        };

    private static IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> MapToDomain(this Dictionary<string, List<ModResponse>> data)
    => data.ToDictionary(
        p => new PartId(p.Key),
        p => (IReadOnlyList<ModEntity>)p.Value.Select(x => x.MapToDomain()).ToList().AsReadOnly());

    public static ModListEntity MapToDomain(this ModListResponse data)
        => new(new(data.Descriptor.Id, data.Descriptor.Name), data.Mods.MapToDomain());

    private static Dictionary<string, List<ModResponse>> MapToApplication(this IReadOnlyDictionary<PartId, IReadOnlyList<ModEntity>> data)
        => data.ToDictionary(
            p => p.Key.Id,
            p => p.Value.Select(x => x.MapToApplication()).ToList());
    public static ModListDescriptorResponse MapToApplication(this ModListDescriptor data)
        => new()
        {
            Id = data.Id,
            Name = data.Name
        };
    public static ModListResponse MapToApplication(this ModListEntity data)
        => new()
        {
            Mods = data.Mods.MapToApplication(),
            Descriptor = data.Descriptor.MapToApplication()
        };



    public static PartSchematicResponse MapToApplication(this ModSchemaPartEntity data)
        => new()
        {
            Id = data.Id.Id,
            Name = data.Name.Name,
            Type = data.Type.Type
        };


}
