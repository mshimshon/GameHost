using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class LoadModFeatureDoneReducer : IReducer<ModListState, LoadModFeatureDoneAction>
{
    public ModListState Reduce(ModListState state, LoadModFeatureDoneAction action)
        => state with
        {
            FeatureInfo = action.ModFeature,
            IsFeatureInfoLoading = false
        };
}
