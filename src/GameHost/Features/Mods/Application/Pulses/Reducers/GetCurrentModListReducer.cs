using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class GetCurrentModListReducer : IReducer<ModListState, GetCurrentModListAction>
{
    public ModListState Reduce(ModListState state, GetCurrentModListAction action)
        => state with
        {
            IsActiveLoading = true
        };
}
