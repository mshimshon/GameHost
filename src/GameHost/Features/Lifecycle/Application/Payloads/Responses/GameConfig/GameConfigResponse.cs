namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

public sealed record GameConfigResponse
{
    public string DisplayName { get; set; } = default!;
    public List<GameConfigParameterResponse>? Parameters { get; set; }
}
