namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses;

internal class InstallerErrorResponse
{
    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
}
