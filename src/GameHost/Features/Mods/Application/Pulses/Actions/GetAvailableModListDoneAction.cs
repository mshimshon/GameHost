using GameHost.Features.Mods.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record GetAvailableModListDoneAction : IAction
{
    public ICollection<ModListDescriptorResponse> Available { get; set; } = new List<ModListDescriptorResponse>();
}
