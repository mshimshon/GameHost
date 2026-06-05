using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameInfo;

public sealed record ExternalGameInfoResponse
{
    public string Name { get; set; } = default!;
    public string? SteamGameId { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public string? SteamServerId { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public string[]? SteamBranch { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool HasModdingWorkshop { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool Modding { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public bool ManualModUpload { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public List<ExternalGameConfigParameterResponse>? StartupParameters { get; set; }
}
