using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Reducers;

internal class InstallGameServerActionReducer : IReducer<InstallationState, InstallGameServerAction>
{

    public InstallationState Reduce(InstallationState state, InstallGameServerAction action)
     => state with
     {
         InProgressInstallation = new()
         {
             CurrentStep = $"Installing {action.GameManifest.DisplayName}...",
             IsInstalling = true
         }
     };
}
