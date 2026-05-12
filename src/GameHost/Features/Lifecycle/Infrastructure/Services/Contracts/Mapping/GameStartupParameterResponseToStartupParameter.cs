using CoreMap;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Enums;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Mapping;

public class GameStartupParameterResponseToStartupParameter : ICoreMapHandler<GameStartupParameterResponse, ConfigParameter>
{
    public ConfigParameter Handler(GameStartupParameterResponse data, ICoreMap alsoMap)
        => new ConfigParameter(data.Key, data.Type.ToLower() switch
        {
            "decimal" => ConfigParameterType.Decimal,
            "bool" or "bool_E" or "bool_S" => ConfigParameterType.Bool,
            "list_str" => ConfigParameterType.List_String,
            "list_double" => ConfigParameterType.List_Decimal,
            "list_int" => ConfigParameterType.List_Interger,
            "integer" => ConfigParameterType.Int,
            _ => ConfigParameterType.String
        });
}
