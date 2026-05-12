using GameHost.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record GetAvailableModListDoneAction : IAction
{
    public IReadOnlyCollection<ModListDescriptor> Available { get; set; } = new List<ModListDescriptor>().AsReadOnly();
}
