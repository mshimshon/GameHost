using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class PopulateAvailableGamesForInstallDoneReducer : IReducer<InstallationState, PopulateAvailableGamesForInstallDoneAction>
{
    public InstallationState Reduce(InstallationState state, PopulateAvailableGamesForInstallDoneAction action)
        => state with
        {
            AvailableGameServers = action.GameManifests?.ToList()?.AsReadOnly(),
            AvailableGameServersLoading = false
        };
}
