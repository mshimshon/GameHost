using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators;
using GameHost.Features.Lifecycle.Application.Providers;
using GameHost.Features.Lifecycle.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

public sealed record GameConfigParameterResponse
{
    public ConfigParameter Key { get; set; } = default!;
    public string Label { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Required { get; set; }
    public bool Editable { get; set; }
    public string? DefaultValue { get; set; }
    public string Category { get; set; } = default!;
    public string? Warning { get; set; }

    [JsonConverter(typeof(ConfigParameterValidatorJsonConverter))]
    public List<BaseConfigParameterValidator>? Validations { get; set; }
}
