using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo.Enums;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;

public sealed record ServerInfoResponse
{
    public ServerStatus Status { get; init; }
    public List<ServerInfoConnectionResponse>? ConnectionPorts { get; }
    public string? Address { get; }
    public DateTime LastUpdate { get; init; }
}
