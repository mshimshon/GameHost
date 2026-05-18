using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;

public sealed record GameInfoResponse
{
    public string Name { get; set; } = default!;
    public bool IsSteam => SteamGameId != default; // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public string? SteamGameId { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public string? SteamServerId { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public string[]? SteamBranch { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool HasModdingWorkshop { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool Modding { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public bool ManualModUpload { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public List<GameConfigParameterResponse>? StartupParameters { get; set; }
}
