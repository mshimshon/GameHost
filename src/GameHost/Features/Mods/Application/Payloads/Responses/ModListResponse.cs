namespace GameHost.Features.Mods.Application.Payloads.Responses;

public class ModListResponse
{
    public ModListDescriptorResponse Descriptor { get; set; } = default!;
    public Dictionary<string, List<ModResponse>> Mods { get; set; } = new();
}
