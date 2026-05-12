using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class GameServerInstallStateLoadedReducer : IReducer<InstallationState, GameServerInstallStateLoadedAction>
{
    public InstallationState Reduce(InstallationState state, GameServerInstallStateLoadedAction action)
        => state with
        {
            InstalledGameServer = action.Info
        };
}
