namespace GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;

internal sealed record InstallerErrorResponse
{
    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
}
