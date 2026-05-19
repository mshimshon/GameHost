namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;

internal sealed record ModFeatureResponse
{
    public bool Modding { get; set; }
    public bool ManualModDownload { get; set; }
}
