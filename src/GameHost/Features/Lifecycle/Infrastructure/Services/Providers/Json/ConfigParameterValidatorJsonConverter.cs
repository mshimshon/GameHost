using GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Providers.Json;

internal class ConfigParameterValidatorJsonConverter : JsonConverter<GameConfigParameterValidator>
{
    public override GameConfigParameterValidator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var typeStr = doc.RootElement.GetProperty("type").GetString();
        var data = doc.RootElement.Deserialize<JsonNode>();
        var rawJson = doc.RootElement.GetRawText();
        if (typeStr == default)
            throw new JsonException($"Missing type {rawJson}");
        if (data == default)
            throw new JsonException($"Invalid type {typeStr}");
        return new GameConfigParameterValidator()
        {
            Content = data
        };
    }

    public override void Write(Utf8JsonWriter writer, GameConfigParameterValidator value, JsonSerializerOptions options)
        => JsonSerializer.Serialize(writer, (object)value, options);
}
