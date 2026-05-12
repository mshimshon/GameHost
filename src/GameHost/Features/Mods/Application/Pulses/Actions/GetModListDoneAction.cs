using GameHost.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record GetModListDoneAction : IAction
{
    public ModListEntity? ModList { get; set; }
}
