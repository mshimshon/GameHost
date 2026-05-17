namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public sealed record GameConfigParameterResponse
{
    public string Key { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Label { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Required { get; set; }
    public bool Editable { get; set; }
    public string? DefaultValue { get; set; }
    public string Category { get; set; } = default!;
    public string? Warning { get; set; }

    public List<GameConfigParameterValidator>? Validations { get; set; }
}
