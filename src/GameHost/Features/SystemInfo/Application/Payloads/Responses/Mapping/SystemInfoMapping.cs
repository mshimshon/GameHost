using GameHost.Features.SystemInfo.Domain.Entites;
using GameHost.Features.SystemInfo.Domain.ValueObjects;

namespace GameHost.Features.SystemInfo.Application.Payloads.Responses.Mapping;

internal static class SystemInfoMapping
{
    public static SystemInfoResponse MapToApplication(this SystemInfoEntity data)
        => new()
        {
            Ram = data.Memory.MapToApplication(),
            Disk = data.Disk.MapToApplication(),
            Processor = data.Processor.MapToApplication()
        };

    public static SystemInfoRamResponse MapToApplication(this SystemMemory data)
        => new()
        {
            Current = data.Current,
            Total = data.Total,
            Percentage = data.Percentage
        };

    public static SystemInfoDiskResponse MapToApplication(this SystemDisk data)
        => new()
        {
            Current = data.Current,
            Total = data.Total,
            Percentage = data.Percentage
        };

    public static SystemInfoProcessorResponse MapToApplication(this SystemProcessor data)
        => new()
        {
            Current = data.Current,
            Cores = data.Cores,
            Model = data.Model
        };
}
