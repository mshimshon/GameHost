using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;

public sealed record GameInfoResponse
{
    public string Name { get; set; } = default!;
    public bool IsSteam => SteamInfo != default; // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public SteamGameId? SteamInfo { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public string? SteamId { get; set; }
    public bool HasModdingWorkshop { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool Modding { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public bool ManualModUpload { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public List<GameConfigParameterResponse>? StartupParameters { get; set; }
}
