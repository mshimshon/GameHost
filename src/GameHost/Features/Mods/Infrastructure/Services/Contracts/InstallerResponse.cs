namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

internal class InstallerResponse<TData>
{
    public TData? Data { get; set; }
    public InstallerErrorResponse? Error { get; set; }
}
