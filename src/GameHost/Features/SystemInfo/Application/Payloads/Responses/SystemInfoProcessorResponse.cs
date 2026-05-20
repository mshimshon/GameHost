namespace GameHost.Features.SystemInfo.Application.Payloads.Responses;

public sealed record SystemInfoProcessorResponse
{
    public float Current { get; set; }
    public string Model { get; set; } = default!;
    public int Cores { get; set; }
}
