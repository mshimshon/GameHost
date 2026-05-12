using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class LoadModFeatureReducer : IReducer<ModListState, LoadModFeatureAction>
{
    public ModListState Reduce(ModListState state, LoadModFeatureAction action)
        => state with { IsFeatureInfoLoading = true };
}
