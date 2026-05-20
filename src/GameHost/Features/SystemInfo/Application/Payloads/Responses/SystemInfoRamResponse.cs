namespace GameHost.Features.SystemInfo.Application.Payloads.Responses;

public sealed record SystemInfoRamResponse
{
    public float Current { get; set; }
    public float Percentage { get; set; }
    public float Total { get; set; }
}
