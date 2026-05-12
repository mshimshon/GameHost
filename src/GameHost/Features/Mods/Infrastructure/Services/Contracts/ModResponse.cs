namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

public sealed class ModResponse
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public List<ModResponse>? Dependencies { get; set; }
}
