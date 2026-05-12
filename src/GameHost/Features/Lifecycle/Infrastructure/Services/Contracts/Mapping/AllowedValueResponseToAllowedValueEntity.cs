using CoreMap;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Mapping;

public class AllowedValueResponseToAllowedValueEntity : ICoreMapHandler<GameStartupParameterAllowedValueResponse, ConfigParameterAllowedValue>
{
    public ConfigParameterAllowedValue Handler(GameStartupParameterAllowedValueResponse data, ICoreMap alsoMap)
        => new(data.Value, data.Label);
}
