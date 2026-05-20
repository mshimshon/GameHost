namespace GameHost.Features.SystemInfo.Infrastructure.Payloads.Response;

internal sealed record SystemInfoRamResponse
{
    public float Current { get; set; }
    public float Total { get; set; }
}
