using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class DeleteModListDoneReducer : IReducer<ModListLocalState, DeleteModListDoneAction>
{
    public ModListLocalState Reduce(ModListLocalState state, DeleteModListDoneAction action)
    {
        var result = state with
        {
            IsCurrentLoading = false
        };
        if (action.ResetCurrent)
            result = result with
            {
                Current = default
            };
        return result;
    }
}
