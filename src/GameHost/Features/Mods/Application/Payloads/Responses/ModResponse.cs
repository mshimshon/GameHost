namespace GameHost.Features.Mods.Application.Payloads.Responses;

public sealed record ModResponse
{
    public string Id { get; set; } = default!;
    public string? Name { get; init; }
    public List<ModResponse>? Dependencies { get; set; } = new();
}
