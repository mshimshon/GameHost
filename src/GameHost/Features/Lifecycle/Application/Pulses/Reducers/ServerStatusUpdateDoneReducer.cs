using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class ServerStatusUpdateDoneReducer : IReducer<ServerState, ServerStatusUpdateDoneAction>
{
    public ServerState Reduce(ServerState state, ServerStatusUpdateDoneAction action)
        => state with
        {
            ServerInfoLastUpdate = DateTime.UtcNow,
            ServerInfo = action.ServerInfo
        };
}
