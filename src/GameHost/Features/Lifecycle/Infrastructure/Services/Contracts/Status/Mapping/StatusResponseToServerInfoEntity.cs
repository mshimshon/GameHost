using CoreMap;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo.Enums;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status.Mapping;

public class StatusResponseToServerInfoEntity : ICoreMapHandler<StatusResponse, ServerInfoEntity>
{
    public ServerInfoEntity Handler(StatusResponse data, ICoreMap alsoMap)
    {
        if (data.ConnectionInfo == default)
            return new ServerInfoEntity(ServerStatus.Unknown);

        var currentStatus = ServerStatus.Unknown;
        if (data.Status == Enums.ServerStatus.Started)
            currentStatus = ServerStatus.Running;
        else if (data.Status == Enums.ServerStatus.Stopped)
            currentStatus = ServerStatus.Stopped;


        var convertedPorts = alsoMap.MapEach(data.ConnectionInfo.PortInfoResponses).To<ConnectionPort>();
        return new ServerInfoEntity(currentStatus, data.ConnectionInfo.Address, convertedPorts);
    }
}


