namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

public sealed record GameConfigInfoResponse
{
    public Dictionary<string, GameConfigResponse>? ConfigDefinitions { get; set; }
}
