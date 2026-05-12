using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record CreateModListAction : IAction
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
