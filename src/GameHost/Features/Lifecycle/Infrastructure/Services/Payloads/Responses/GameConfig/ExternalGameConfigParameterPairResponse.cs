namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record ExternalGameConfigParameterPairResponse
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
