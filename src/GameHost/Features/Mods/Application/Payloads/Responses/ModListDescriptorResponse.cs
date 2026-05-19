namespace GameHost.Features.Mods.Application.Payloads.Responses;

public sealed record ModListDescriptorResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
