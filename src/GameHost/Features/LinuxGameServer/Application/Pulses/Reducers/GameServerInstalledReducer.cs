using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class GameServerInstalledReducer : IReducer<InstallationState, GameServerInstalledAction>
{
    public InstallationState Reduce(InstallationState state, GameServerInstalledAction action)
       => state with
       {
           InstalledGameServer = new()
           {
               Id = action.GameServerInstalled.Id,
               InstallDate = action.GameServerInstalled.InstallDate
           },
           InProgressInstallation = default
       };
}
