using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo.Enums;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo.Mapping;

internal static class ServerInfoMappingExt
{
    public static Application.Payloads.Responses.ServerInfo.ServerInfoResponse MapToApplication(this ServerInfoResponse data)
        => new()
        {
            ConnectionPorts = data.ConnectionPorts?.Select(MapToApplication).ToList(),
            Address = data.Address,
            LastUpdate = data.LastUpdate,
            Status = data.Status.MapToApplication()
        };

    public static Application.Payloads.Responses.ServerInfo.ServerInfoConnectionResponse MapToApplication(this ServerInfoConnectionResponse data)
        => new()
        {
            Name = data.Name,
            Port = data.Port,
            Protocol = data.Protocol
        };

    public static Application.Payloads.Responses.ServerInfo.Enums.ServerStatus MapToApplication(this ServerStatus data)
        => data switch
        {
            ServerStatus.Running => Application.Payloads.Responses.ServerInfo.Enums.ServerStatus.Running,
            ServerStatus.Stopped => Application.Payloads.Responses.ServerInfo.Enums.ServerStatus.Stopped,
            ServerStatus.Failed => Application.Payloads.Responses.ServerInfo.Enums.ServerStatus.Failed,
            ServerStatus.Restarting => Application.Payloads.Responses.ServerInfo.Enums.ServerStatus.Restarting,
            _ => Application.Payloads.Responses.ServerInfo.Enums.ServerStatus.Unknown
        };


}
