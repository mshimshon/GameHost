using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Enums;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.AllowedValues;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.LengthConstraint;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig.Mapping;

internal static class GameConfigMappingExt
{
    public static GameConfigInfoResponse MapToApplication(this ExternalGameConfigInfoResponse data)
        => new()
        {
            ConfigDefinitions = data.ConfigDefinitions?.ToDictionary(p => p.Key, p => p.Value.MapToApplication())
        };

    public static GameConfigParameterResponse MapToApplication(this ExternalGameConfigParameterResponse data)
        => new()
        {
            Category = data.Category,
            DefaultValue = data.DefaultValue,
            Description = data.Description,
            Editable = data.Editable,
            Key = data.Key,
            Label = data.Label,
            Required = data.Required,
            Type = MapTypeEnum(data),
            Validations = data.Validations?.Select(MapToApplication).ToList(),
            Warning = data.Warning
        };


    private static ConfigParameterType MapTypeEnum(ExternalGameConfigParameterResponse data)
    => data.Type switch
    {
        "decimal" => ConfigParameterType.Decimal,
        "bool_S" => ConfigParameterType.Bool_String,
        "bool_E" => ConfigParameterType.Bool_Explicit,
        "bool" => ConfigParameterType.Bool,
        "integer" => ConfigParameterType.Int,
        "list_decimal" => ConfigParameterType.List_Decimal,
        "list_int" => ConfigParameterType.List_Int,
        "list_str" => ConfigParameterType.List_String,
        _ => ConfigParameterType.String
    };

    public static Application.Payloads.Responses.GameConfig.GameConfigResponse MapToApplication(this ExternalGameConfigResponse data)
        => new()
        {
            DisplayName = data.DisplayName,
            Parameters = data.Parameters?.Select(MapToApplication).ToList()
        };
    public static Application.Payloads.Responses.GameConfig.GameConfigParameterPairHostResponse MapToApplication(this ExternalGameConfigParameterPairHostResponse data)
        => new()
        {
            DefaultValue = data.DefaultValue,
            ForcedValue = data.ForcedValue,
            Key = data.Key
        };

    public static Application.Payloads.Responses.GameConfig.GameConfigParameterPairResponse MapToApplication(this ExternalGameConfigParameterPairResponse data)
    => new()
    {
        Value = data.Value,
        Key = data.Key
    };

    public static BaseConfigParameterValidator MapToApplication(this ExternalGameConfigParameterValidator validator)
    {
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        var typeStr = validator.Type;
        JsonObject rebuiltPartial = new JsonObject();
        var parsedNode = JsonNode.Parse(validator.Data);
        rebuiltPartial["data"] = parsedNode?.DeepClone();
        rebuiltPartial["type"] = typeStr;

        BaseConfigParameterValidator? result = typeStr switch
        {
            ValidatorDefinitions.LENGTH_CONSTRAINT => JsonSerializer.Deserialize<ParameterLengthConstraintValidator>(rebuiltPartial, options),
            ValidatorDefinitions.ALLOWED_VALUES => JsonSerializer.Deserialize<ParameterAllowedValuesValidator>(rebuiltPartial, options),
            _ => default
        };
        if (result == default)
            throw new JsonException($"Unknown type: {typeStr}");
        return result;
    }
}
