namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Contracts.Responses;

internal class InstallerResponse<TData>
{
    public TData? Data { get; set; }
    public InstallerErrorResponse? Error { get; set; }
}
