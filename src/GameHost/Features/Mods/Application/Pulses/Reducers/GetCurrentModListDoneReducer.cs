using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class GetCurrentModListDoneReducer : IReducer<ModListState, GetCurrentModListDoneAction>
{
    public ModListState Reduce(ModListState state, GetCurrentModListDoneAction action)
        => state with
        {
            Active = action.Current,
            IsActiveLoading = false
        };
}
