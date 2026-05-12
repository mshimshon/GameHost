using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class SaveModListReducer : IReducer<ModListLocalState, SaveModListAction>
{
    public ModListLocalState Reduce(ModListLocalState state, SaveModListAction action)
        => state with
        {
            Current = default,
            IsCurrentLoading = true
        };
}
