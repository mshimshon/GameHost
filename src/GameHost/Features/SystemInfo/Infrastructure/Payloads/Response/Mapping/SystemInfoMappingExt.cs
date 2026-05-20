using GameHost.Features.SystemInfo.Domain.ValueObjects;

namespace GameHost.Features.SystemInfo.Infrastructure.Payloads.Response.Mapping;

internal static class SystemInfoMappingExt
{
    public static SystemDisk MapToDomain(this SystemInfoDiskResponse data)
        => new(data.Current, data.Total);
    public static SystemMemory MapToDomain(this SystemInfoRamResponse data)
    => new(data.Current, data.Total);
    public static SystemProcessor MapToDomain(this SystemInfoProcessorResponse data)
    => new(data.Current, data.Cores, data.Model);
}
