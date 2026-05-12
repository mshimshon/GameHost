namespace GameHost.Features.Lifecycle.Domain.ValueObjects;

public sealed record SteamGameId
{
    public SteamGameId(string steamid, bool hasModdingWorkshop)
    {
        Steamid = steamid;
        HasModdingWorkshop = hasModdingWorkshop;
    }

    public string Steamid { get; }
    public bool HasModdingWorkshop { get; }
}
