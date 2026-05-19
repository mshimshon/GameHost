using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses.Mapping;

internal static class InstallMappingExt
{
    public static GameServerInstallProgressEntity MapToDomain(this GameServerInstallProgressResponse data)
    => new()
    {
        DisplayName = data.DisplayName,
        CurrentStep = data.CurrentStep,
        FailureReason = data.FailureReason,
        Id = new(data.Id),
        IsInstalling = data.IsInstalling
    };

    public static GameServerInfoEntity MapToDomain(this GameServerInfoResponse data)
        => new()
        {
            InstallDate = data.InstallDate,
            DisplayName = data.DisplayName,
            Id = new(data.Id),
        };
}
