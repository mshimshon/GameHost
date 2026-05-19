namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;

internal sealed record ModSchemaResponse
{
    public Dictionary<string, ModSchemaPartResponse> ModSchema { get; set; } = default!;
}
