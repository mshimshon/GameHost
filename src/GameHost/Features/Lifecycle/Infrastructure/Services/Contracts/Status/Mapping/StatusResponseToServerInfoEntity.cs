using CoreMap;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status.Mapping;

public class StatusResponseToServerInfoEntity : ICoreMapHandler<StatusResponse, ServerInfoEntity>
{
    public ServerInfoEntity Handler(StatusResponse data, ICoreMap alsoMap)
    {
        if (data.ConnectionInfo == default)
            return new ServerInfoEntity(Domain.Enums.Status.Unknown);

        var currentStatus = Domain.Enums.Status.Unknown;
        if (data.Status == Enums.ServerStatus.Started)
            currentStatus = Domain.Enums.Status.Running;
        else if (data.Status == Enums.ServerStatus.Stopped)
            currentStatus = Domain.Enums.Status.Stopped;


        var convertedPorts = alsoMap.MapEach(data.ConnectionInfo.PortInfoResponses).To<ConnectionPort>();
        return new ServerInfoEntity(currentStatus, data.ConnectionInfo.Address, convertedPorts);
    }
}


