using GameHost.Features.Lifecycle.Infrastructure.Services.Providers.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts;

public sealed record GameStartupParameterHostDefResponse
{
    public string Key { get; set; } = default!;
    public bool Editable { get; set; }
    public bool Visible { get; set; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? ForcedValue { get; set; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? DefaultValue { get; set; }
}
