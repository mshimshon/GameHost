namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;

internal sealed record ModSchemaPartResponse
{
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
}
