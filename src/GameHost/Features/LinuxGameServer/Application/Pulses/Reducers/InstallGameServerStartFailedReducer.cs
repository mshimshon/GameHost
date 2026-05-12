using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class InstallGameServerStartFailedReducer : IReducer<InstallationState, InstallGameServerStartFailedAction>
{
    public InstallationState Reduce(InstallationState state, InstallGameServerStartFailedAction action)
        => state with
        {
            InstalledGameServer = default,
            InProgressInstallation = default
        };
}
