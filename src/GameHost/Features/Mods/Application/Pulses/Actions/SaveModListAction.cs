using GameHost.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record SaveModListAction : IAction
{
    public ModListEntity ModListEntity { get; set; } = default!;
}
