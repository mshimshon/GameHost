using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameInfo;

public sealed record GameInfoResponse
{
    public string Name { get; set; } = default!;
    public string? SteamId { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool HasModdingWorkshop { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO STEAM MODULE
    public bool Modding { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public bool ManualModUpload { get; set; } // TODO: REMOVE AS IT NOT RELEVENT FOR LIFECYCLE MOVE TO MOD MODULE
    public List<GameConfigParameterResponse>? StartupParameters { get; set; }
}
