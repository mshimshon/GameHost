using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class GetModListReducer : IReducer<ModListLocalState, GetModListAction>
{
    public ModListLocalState Reduce(ModListLocalState state, GetModListAction action)
        => state with
        {
            IsCurrentLoading = true
        };
}
