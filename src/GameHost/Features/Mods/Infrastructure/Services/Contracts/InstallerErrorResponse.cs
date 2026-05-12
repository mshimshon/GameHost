namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

internal class InstallerErrorResponse
{
    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
}
