namespace GameHost.Features.Mods.Web.Hooks.Events.Dto;

internal sealed record InstallationStateResponse
{
    public bool IsInstallationCompleted { get; init; }
}
