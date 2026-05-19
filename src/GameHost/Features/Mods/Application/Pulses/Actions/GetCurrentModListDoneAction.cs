using GameHost.Features.Mods.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

internal sealed record GetCurrentModListDoneAction : IAction
{
    public ModListDescriptorResponse? Current { get; set; }
}
