using GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status.Enums;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status;

public record StatusResponse
{
    public ServerStatus Status { get; set; } = ServerStatus.Unknown;
    public ConnectionInfoResponse? ConnectionInfo { get; set; }
}