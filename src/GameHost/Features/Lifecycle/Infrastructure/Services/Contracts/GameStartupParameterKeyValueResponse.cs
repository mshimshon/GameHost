namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts;

public sealed record GameStartupParameterKeyValueResponse
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
