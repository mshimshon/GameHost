using GameHost.Features.Lifecycle.Infrastructure.Services.Providers.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record ExternalGameConfigParameterPairHostResponse
{
    public string Key { get; init; } = default!;

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? ForcedValue { get; set; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? DefaultValue { get; set; }
}
