using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class FetchStartupParametersDoneReducer : IReducer<GameInfoState, FetchStartupParametersDoneAction>
{
    public GameInfoState Reduce(GameInfoState state, FetchStartupParametersDoneAction action)
        => state with
        {
            StartupParameters = action.StartupParameters,
            SavedParametersLoaded = true
        };
}
