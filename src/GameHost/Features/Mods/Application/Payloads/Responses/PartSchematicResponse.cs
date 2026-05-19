namespace GameHost.Features.Mods.Application.Payloads.Responses;

public sealed record PartSchematicResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
}
