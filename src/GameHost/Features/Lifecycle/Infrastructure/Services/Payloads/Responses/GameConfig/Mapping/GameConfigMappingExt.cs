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
            Type = data.Type,
            Validations = data.Validations?.Select(MapToApplication).ToList(),
            Warning = data.Warning
        };

    public static Application.Payloads.Responses.GameConfig.GameConfigResponse MapToApplication(this GameConfigResponse data)
        => new()
        {
            DisplayName = data.DisplayName,
            Parameters = data.Parameters?.Select(MapToApplication).ToList()
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
            "LengthConstraint" => JsonSerializer.Deserialize<ParameterLengthConstraintValidator>(validator.Content, options),
            "AllowedValues" => JsonSerializer.Deserialize<ParameterAllowedValuesValidator>(validator.Content, options),
            _ => default
        };
        if (result == default)
            throw new JsonException($"Unknown type: {typeStr}");
        return result;
    }
}
