using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Application.Payloads.Responses.Mapping;

internal static class ManifestMappingExt
{
    public static GameManifestResponse MapToApplication(this GameManifestEntity data)
        => new()
        {
            DisplayName = data.DisplayName,
            Id = data.Id.Value,
            DistroCompatibility = data.DistroCompatibility.ToList(),
            Icon = data.Icon,
            InstallerSource = data.InstallerSource,
            InstallerName = data.InstallerName
        };

    public static GameManifestEntity MapToDomain(this GameManifestResponse data)
    => new()
    {

        DisplayName = data.DisplayName,
        Id = new(data.Id),
        DistroCompatibility = data.DistroCompatibility.ToList(),
        Icon = data.Icon,
        InstallerSource = data.InstallerSource,
        InstallerName = data.InstallerName
    };
}
