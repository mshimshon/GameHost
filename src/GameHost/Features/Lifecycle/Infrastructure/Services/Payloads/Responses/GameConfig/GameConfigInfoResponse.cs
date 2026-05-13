namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record GameConfigInfoResponse
{
    public Dictionary<string, GameConfigResponse>? ConfigDefinitions { get; set; }
}
