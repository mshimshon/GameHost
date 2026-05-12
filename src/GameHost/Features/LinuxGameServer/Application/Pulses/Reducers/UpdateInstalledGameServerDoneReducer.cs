using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class UpdateInstalledGameServerDoneReducer : IReducer<InstallationState, UpdateInstalledGameServerDoneAction>
{
    public InstallationState Reduce(InstallationState state, UpdateInstalledGameServerDoneAction action)
        => state with
        {
            InstalledGameServer = action.GameServerInfo,
            IsInstalledGameDiskLoaded = true
        };
}
