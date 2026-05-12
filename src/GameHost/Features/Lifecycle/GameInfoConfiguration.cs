namespace GameHost.Features.Lifecycle;

public class GameInfoConfiguration
{
    public string Name { get; init; } = default!;
    public string LifeCycleProvider { get; init; } = default!;
    public string? SteamGameAppId { get; init; }
    public string? SteamServerAppId { get; init; }
    public bool SteamUseWorkshop { get; init; }
    public bool CanBeModded { get; init; }
    public bool RequiredManualModUpload { get; init; }
}
