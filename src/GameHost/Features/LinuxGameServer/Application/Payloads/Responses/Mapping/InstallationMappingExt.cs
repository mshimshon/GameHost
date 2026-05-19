using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Application.Payloads.Responses.Mapping;

internal static class InstallationMappingExt
{
    public static GameServerInfoResponse MapToApplication(this GameServerInfoEntity data)
     => new()
     {
         Id = data.Id.Value,
         DisplayName = data.DisplayName,
         InstallDate = data.InstallDate
     };

    public static GameServerInfoEntity MapToDomain(this GameServerInfoResponse data)
         => new()
         {
             Id = new(data.Id),
             DisplayName = data.DisplayName,
             InstallDate = data.InstallDate
         };

    public static GameServerInstallProgressResponse MapToApplication(this GameServerInstallProgressEntity data)
        => new GameServerInstallProgressResponse()
        {
            Id = data.Id.Value,
            CurrentStep = data.CurrentStep,
            DisplayName = data.DisplayName,
            FailureReason = data.FailureReason,
            IsInstalling = data.IsInstalling
        };

    public static GameServerInstallProgressEntity MapToDomain(this GameServerInstallProgressResponse data)
        => new()
        {
            Id = new(data.Id),
            CurrentStep = data.CurrentStep,
            DisplayName = data.DisplayName,
            FailureReason = data.FailureReason,
            IsInstalling = data.IsInstalling
        };

}
