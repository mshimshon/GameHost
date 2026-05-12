using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class CloseModListReducer : IReducer<ModListLocalState, CloseModListAction>
{
    public ModListLocalState Reduce(ModListLocalState state, CloseModListAction action)
        => state with
        {
            Current = default
        };
}
