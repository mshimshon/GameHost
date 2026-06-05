
using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo.Enums;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo;

public sealed record ExternalServerInfoResponse
{
    public ExternalServerStatus Status { get; set; }
    public List<ExternalServerInfoConnectionResponse>? ConnectionPorts { get; set; }
    public string? Address { get; set; }
    public DateTime LastUpdate { get; set; }
}
