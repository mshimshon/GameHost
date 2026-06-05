using GameHost.Features.Lifecycle.Infrastructure.Services.Providers.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record ExternalGameConfigParameterResponse
{
    public string Key { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Label { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Required { get; set; }
    public bool Editable { get; set; }
    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? DefaultValue { get; set; }
    public string Category { get; set; } = default!;
    public string? Warning { get; set; }

    public List<ExternalGameConfigParameterValidator>? Validations { get; set; }
}
