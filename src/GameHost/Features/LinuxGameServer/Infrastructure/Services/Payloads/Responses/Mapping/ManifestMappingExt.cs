using GameHost.Features.LinuxGameServer.Domain.Entities;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses.Mapping;

internal static class ManifestMappingExt
{
    public static GameManifestEntity MapToDomain(this GameManifestResponse data)
        => new GameManifestEntity()
        {
            DistroCompatibility = data.CompatibleDistro?.ToList()?.AsReadOnly() ?? new List<string>().AsReadOnly(),
            Icon = data.Icon,
            DisplayName = data.DisplayName,
            Id = new(data.Id),
            InstallerSource = data.InstallerSource,
            InstallerName = data.InstallerName
        };
}
