namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

internal sealed record ModFeatureResponse
{
    public bool RequiredManualDownload { get; set; }
}
