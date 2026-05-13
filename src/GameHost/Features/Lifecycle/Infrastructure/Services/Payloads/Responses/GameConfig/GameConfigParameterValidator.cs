using System.Text.Json.Nodes;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;


public sealed record GameConfigParameterValidator
{
    public JsonNode Content { get; set; } = default!;
}
