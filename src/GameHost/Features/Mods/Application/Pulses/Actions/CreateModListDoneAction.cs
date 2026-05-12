using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record CreateModListDoneAction : IAction
{
    public bool Failed { get; set; }
}
