using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record DeleteModListDoneAction : IAction
{
    public bool ResetCurrent { get; set; }
}
