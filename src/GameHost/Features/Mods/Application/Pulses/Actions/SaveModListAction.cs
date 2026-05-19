using GameHost.Features.Mods.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record SaveModListAction : IAction
{
    public ModListResponse ModList { get; set; } = default!;
}
