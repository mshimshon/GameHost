namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

public sealed class ModListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Dictionary<string, List<ModResponse>> Mods { get; set; } = new();
}
