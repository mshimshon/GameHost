using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class GetModListDoneReducer : IReducer<ModListLocalState, GetModListDoneAction>
{
    public ModListLocalState Reduce(ModListLocalState state, GetModListDoneAction action)
        => state with
        {
            Current = action.ModList,
            IsCurrentLoading = false
        };
}
