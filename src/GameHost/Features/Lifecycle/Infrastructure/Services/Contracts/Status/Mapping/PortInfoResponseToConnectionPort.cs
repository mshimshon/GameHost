using CoreMap;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status.Mapping;

internal class PortInfoResponseToConnectionPort : ICoreMapHandler<PortInfoResponse, ConnectionPort>
{
    public ConnectionPort Handler(PortInfoResponse data, ICoreMap alsoMap)
        => new(data.Name, data.Port, data.Protocol);
}
