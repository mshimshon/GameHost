using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.AllowedValues;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.LengthConstraint;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Application.Providers;

internal class ConfigParameterValidatorJsonConverter : JsonConverter<BaseConfigParameterValidator>
{
    public override BaseConfigParameterValidator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var typeStr = doc.RootElement.GetProperty("type").GetString();
        var rawJson = doc.RootElement.GetRawText();
        BaseConfigParameterValidator? result = typeStr switch
        {
            "LengthConstraint" => JsonSerializer.Deserialize<ParameterLengthConstraintValidator>(rawJson, options),
            "AllowedValues" => JsonSerializer.Deserialize<ParameterAllowedValuesValidator>(rawJson, options),
            _ => default
        };
        if (result == default)
            throw new JsonException($"Unknown type: {typeStr}");
        return result;
    }

    public override void Write(Utf8JsonWriter writer, BaseConfigParameterValidator value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, (object)value, options);
}
