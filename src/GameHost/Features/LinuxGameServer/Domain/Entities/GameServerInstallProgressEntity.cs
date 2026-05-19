using GameHost.Features.LinuxGameServer.Domain.ValueObjects;

namespace GameHost.Features.LinuxGameServer.Domain.Entities;

public sealed record GameServerInstallProgressEntity
{
    public string? FailureReason { get; init; }
    public bool IsInstalling { get; init; }
    public string CurrentStep { get; init; } = default!;
    public ServerGameId Id { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
}
