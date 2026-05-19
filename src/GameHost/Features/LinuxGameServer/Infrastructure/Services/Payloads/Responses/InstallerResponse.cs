namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses;

internal class InstallerResponse<TData>
{
    public TData? Data { get; set; }
    public InstallerErrorResponse? Error { get; set; }
}
