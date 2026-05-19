namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;

internal sealed record ModResponse
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public List<ModResponse>? Dependencies { get; set; }
}
