using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class DeleteModListReducer : IReducer<ModListLocalState, DeleteModListAction>
{
    public ModListLocalState Reduce(ModListLocalState state, DeleteModListAction action)
        => state with
        {
            IsCurrentLoading = true
        };
}
