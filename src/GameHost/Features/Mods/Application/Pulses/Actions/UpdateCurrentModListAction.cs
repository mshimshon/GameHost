using GameHost.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record UpdateCurrentModListAction : IAction
{
    public ModListDescriptor? Current { get; set; }
}
