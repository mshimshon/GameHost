namespace GameHost.Features.LinuxGameServer.Domain.Entities;

public record GameServerInfoEntity
{
    public string Id { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
    public DateTime InstallDate { get; init; }


}
