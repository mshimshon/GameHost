using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo.Enums;
using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo.Enums;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo.Mapping;

internal static class ServerInfoMappingExt
{
    public static ServerInfoResponse MapToApplication(this ExternalServerInfoResponse data)
        => new()
        {
            ConnectionPorts = data.ConnectionPorts?.Select(MapToApplication).ToList(),
            Address = data.Address,
            LastUpdate = data.LastUpdate,
            Status = data.Status.MapToApplication()
        };

    public static ServerInfoConnectionResponse MapToApplication(this ExternalServerInfoConnectionResponse data)
        => new()
        {
            Name = data.Name,
            Port = data.Port,
            Protocol = data.Protocol
        };

    public static ServerStatus MapToApplication(this ExternalServerStatus data)
        => data switch
        {
            ExternalServerStatus.Running => ServerStatus.Running,
            ExternalServerStatus.Stopped => ServerStatus.Stopped,
            ExternalServerStatus.Failed => ServerStatus.Failed,
            ExternalServerStatus.Restarting => ServerStatus.Restarting,
            _ => ServerStatus.Unknown
        };


}
