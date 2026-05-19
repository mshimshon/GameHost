namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses;

public sealed record GameManifestResponse
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string? Icon { get; set; }
    public List<string> CompatibleDistro { get; set; } = default!;
    public string InstallerName { get; set; } = default!;
    public string InstallerSource { get; set; } = default!;
}
