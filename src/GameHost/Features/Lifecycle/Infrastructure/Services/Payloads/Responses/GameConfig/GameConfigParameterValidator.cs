using GameHost.Features.Lifecycle.Infrastructure.Services.Providers.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;


[JsonConverter(typeof(ConfigParameterValidatorJsonConverter))]
public sealed record GameConfigParameterValidator
{
    public JsonNode Content { get; set; } = default!;
}
