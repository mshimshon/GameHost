using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record GetModListAction : IAction
{
    public Guid Id { get; set; }
}
