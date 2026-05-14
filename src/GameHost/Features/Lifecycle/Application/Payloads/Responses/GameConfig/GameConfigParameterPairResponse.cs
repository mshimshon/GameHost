namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

public sealed record GameConfigParameterPairResponse
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
