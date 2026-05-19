namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;

internal sealed record InstallerResponse<TData>
{
    public TData? Data { get; set; }
    public InstallerErrorResponse? Error { get; set; }
}
