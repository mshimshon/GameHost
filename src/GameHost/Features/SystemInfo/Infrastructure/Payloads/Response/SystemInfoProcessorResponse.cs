namespace GameHost.Features.SystemInfo.Infrastructure.Payloads.Response;

internal sealed record SystemInfoProcessorResponse
{
    public float Current { get; set; }
    public string Model { get; set; } = default!;
    public int Cores { get; set; }
}
