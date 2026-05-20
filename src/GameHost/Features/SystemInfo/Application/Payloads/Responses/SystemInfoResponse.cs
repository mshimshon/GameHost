namespace GameHost.Features.SystemInfo.Application.Payloads.Responses;

public sealed record SystemInfoResponse
{
    public SystemInfoDiskResponse Disk { get; set; } = default!;
    public SystemInfoProcessorResponse Processor { get; set; } = default!;
    public SystemInfoRamResponse Ram { get; set; } = default!;
}
