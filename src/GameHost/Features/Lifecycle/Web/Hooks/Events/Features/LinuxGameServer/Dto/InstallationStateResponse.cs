namespace GameHost.Features.Lifecycle.Web.Hooks.Events.Features.LinuxGameServer.Dto;

internal sealed record InstallationStateResponse
{
    public bool IsInstallationCompleted { get; init; }
    public bool IsInstalledGameDiskLoaded { get; init; }
    public bool IsProgressDiskLoaded { get; init; }
}
