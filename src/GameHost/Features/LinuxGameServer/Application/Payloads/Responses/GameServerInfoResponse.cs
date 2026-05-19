namespace GameHost.Features.LinuxGameServer.Application.Payloads.Responses;

public sealed record GameServerInfoResponse
{
    public string Id { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public DateTime InstallDate { get; set; }

}
