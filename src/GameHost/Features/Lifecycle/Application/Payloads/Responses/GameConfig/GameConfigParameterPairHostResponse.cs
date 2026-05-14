namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

public sealed record GameConfigParameterPairHostResponse
{
    public string Key { get; init; } = default!;
    public string? ForcedValue { get; set; }
    public string? DefaultValue { get; set; }
}
