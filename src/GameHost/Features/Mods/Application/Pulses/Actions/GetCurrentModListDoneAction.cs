using GameHost.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

internal sealed record GetCurrentModListDoneAction : IAction
{
    public ModListDescriptor? Current { get; set; }
}
