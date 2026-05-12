using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class PopulateAvailableGamesForInstallReducer : IReducer<InstallationState, PopulateAvailableGamesForInstallAction>
{
    public InstallationState Reduce(InstallationState state, PopulateAvailableGamesForInstallAction action)
        => state with
        {
            AvailableGameServersLoading = true
        };
}
