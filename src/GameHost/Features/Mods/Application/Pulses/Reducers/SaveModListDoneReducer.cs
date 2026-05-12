using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class SaveModListDoneReducer : IReducer<ModListLocalState, SaveModListDoneAction>
{
    public ModListLocalState Reduce(ModListLocalState state, SaveModListDoneAction action)
        => state with
        {
            IsCurrentLoading = false
        };
}
