namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.AllowedValues;

public sealed record ParameterAllowedValuesData
{
    public string Value { get; set; } = default!;
    public string Label { get; set; } = default!;
}
