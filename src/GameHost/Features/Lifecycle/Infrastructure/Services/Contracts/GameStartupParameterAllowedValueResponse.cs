namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts;

public sealed record GameStartupParameterAllowedValueResponse
{
    public string Value { get; init; } = default!;
    public string Label { get; init; } = default!;
}
