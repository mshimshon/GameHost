namespace GameHost.Features.Mods.Application.Contracts.Responses;

public sealed record ModFeatureResponse
{
    public bool IsEnabled { get; set; }
    public bool IsManualDownload { get; set; }
}
