using GameHost.Features.Lifecycle.Infrastructure.Services.Providers.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;


public sealed record ExternalGameConfigParameterValidator
{
    public string Type { get; set; } = default!;
    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string Data { get; set; } = default!;
}
