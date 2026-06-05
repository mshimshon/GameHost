namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record ExternalGameConfigInfoResponse
{
    public Dictionary<string, ExternalGameConfigResponse>? ConfigDefinitions { get; set; }
}
