using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class GameServerInstallFailedReducer : IReducer<InstallationState, GameServerInstallFailedAction>
{
    public InstallationState Reduce(InstallationState state, GameServerInstallFailedAction action)
    => state with
    {
        InProgressInstallation = new Models.GameServerInstallProcessModel()
        {
            FailureReason = action.FailureReason,
            Id = action.Id,
            DisplayName = action.DisplayName
        },
        InstalledGameServer = default
    };

}
