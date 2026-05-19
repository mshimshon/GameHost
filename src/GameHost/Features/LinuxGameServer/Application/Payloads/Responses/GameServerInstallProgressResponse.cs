namespace GameHost.Features.LinuxGameServer.Application.Payloads.Responses;

public sealed record GameServerInstallProgressResponse
{
    public bool Failed => !string.IsNullOrWhiteSpace(FailureReason);
    public string? FailureReason { get; set; }
    public bool IsInstalling { get; set; }
    public string CurrentStep { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
