using CoreMap;
using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using GameHost.Features.LinuxGameServer.Application.Models;

namespace GameHost.Features.LinuxGameServer.Application.Contracts.Responses.Mapping;

internal class InstallationProgressToInstallationProcessModel : ICoreMapHandler<InstallationProgressStateDto, GameServerInstallProcessModel>
{
    public GameServerInstallProcessModel Handler(InstallationProgressStateDto data, ICoreMap alsoMap)
        => new GameServerInstallProcessModel()
        {
            CurrentStep = data.CurrentStep,
            DisplayName = data.DisplayName,
            FailureReason = data.FailureReason,
            Id = data.Id,
            IsInstalling = data.IsInstalling
        };
}
