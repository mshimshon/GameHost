
using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo.Enums;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo;

public sealed record ServerInfoResponse
{
    public ServerStatus Status { get; set; }
    public List<ServerInfoConnectionResponse>? ConnectionPorts { get; set; }
    public string? Address { get; set; }
    public DateTime LastUpdate { get; set; }
}
