namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record ExternalGameConfigResponse
{
    public string DisplayName { get; set; } = default!;
    public List<ExternalGameConfigParameterResponse>? Parameters { get; set; }
}
