namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts;

public sealed record GameStartupParameterRelatedToResponse
{
    public string Key { get; init; } = default!;
    public string Constraint { get; init; } = default!;
    public string Message { get; init; } = default!;
}
