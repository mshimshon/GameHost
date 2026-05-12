using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class UpdateCurrentModListDoneReducer : IReducer<ModListState, UpdateCurrentModListDoneAction>
{
    public ModListState Reduce(ModListState state, UpdateCurrentModListDoneAction action)
        => state with
        {
            Active = action.Current,
            IsActiveLoading = false
        };
}
