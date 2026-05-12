using GameHost.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record UpdateCurrentModListDoneAction : IAction
{
    public ModListDescriptor? Current { get; set; }
}