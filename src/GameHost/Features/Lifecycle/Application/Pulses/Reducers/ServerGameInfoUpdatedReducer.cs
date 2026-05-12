using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class ServerGameInfoUpdatedReducer : IReducer<GameInfoState, ServerGameInfoUpdateDoneAction>
{

    public GameInfoState Reduce(GameInfoState state, ServerGameInfoUpdateDoneAction action)
        => state with
        {
            GameInfo = action.GameInfo
        };
}
