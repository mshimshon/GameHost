using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;
using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig.Mapping;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameInfo.Mapping;

internal static class GameInfoMappingExt
{
    public static GameInfoResponse MapToApplication(this ExternalGameInfoResponse data)
        => new()
        {
            HasModdingWorkshop = data.HasModdingWorkshop,
            ManualModUpload = data.ManualModUpload,
            SteamServerId = data.SteamServerId,
            Modding = data.Modding,
            Name = data.Name,
            StartupParameters = data.StartupParameters?.Select(GameConfigMappingExt.MapToApplication).ToList(),
            SteamGameId = data.SteamGameId,
            SteamBranch = data.SteamBranch
        };
}
