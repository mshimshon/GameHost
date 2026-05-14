using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.AllowedValues;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.LengthConstraint;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig.Mapping;

internal static class GameConfigMappingExt
{
    public static Application.Payloads.Responses.GameConfig.GameConfigInfoResponse MapToApplication(this GameConfigInfoResponse data)
        => new()
        {
            ConfigDefinitions = data.ConfigDefinitions?.ToDictionary(p => p.Key, p => p.Value.MapToApplication())
        };

    public static Application.Payloads.Responses.GameConfig.GameConfigParameterResponse MapToApplication(this GameConfigParameterResponse data)
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


    private static Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType MapTypeEnum(GameConfigParameterResponse data)
    => data.Type switch
    {
        "decimal" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.Decimal,
        "bool_S" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.Bool_String,
        "bool_E" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.Bool_Explicit,
        "bool" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.Bool,
        "integer" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.Int,
        "list_decimal" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.List_Decimal,
        "list_int" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.List_Int,
        "list_str" => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.List_String,
        _ => Application.Payloads.Responses.GameConfig.Enums.ConfigParameterType.String
    };

    public static Application.Payloads.Responses.GameConfig.GameConfigResponse MapToApplication(this GameConfigResponse data)
        => new()
        {
            DisplayName = data.DisplayName,
            Parameters = data.Parameters?.Select(MapToApplication).ToList()
        };
    public static Application.Payloads.Responses.GameConfig.GameConfigParameterPairHostResponse MapToApplication(this GameConfigParameterPairHostResponse data)
        => new()
        {
            DefaultValue = data.DefaultValue,
            ForcedValue = data.ForcedValue,
            Key = data.Key
        };

    public static Application.Payloads.Responses.GameConfig.GameConfigParameterPairResponse MapToApplication(this GameConfigParameterPairResponse data)
    => new()
    {
        Value = data.Value,
        Key = data.Key
    };

    public static Application.Payloads.Responses.GameConfig.Validators.BaseConfigParameterValidator MapToApplication(this GameConfigParameterValidator validator)
    {
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        var typeStr = validator.Content["type"]?.ToString() ??
            validator.Content["Type"]?.ToString() ??
            throw new JsonException($"Bad Format: {nameof(GameConfigParameterValidator)}"); ;
        Application.Payloads.Responses.GameConfig.Validators.BaseConfigParameterValidator? result = typeStr switch
        {
            ValidatorDefinitions.LENGTH_CONSTRAINT => JsonSerializer.Deserialize<ParameterLengthConstraintValidator>(validator.Content, options),
            ValidatorDefinitions.ALLOWED_VALUES => JsonSerializer.Deserialize<ParameterAllowedValuesValidator>(validator.Content, options),
            _ => default
        };
        if (result == default)
            throw new JsonException($"Unknown type: {typeStr}");
        return result;
    }
}
