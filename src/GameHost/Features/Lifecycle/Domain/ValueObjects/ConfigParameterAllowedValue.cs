namespace GameHost.Features.Lifecycle.Domain.ValueObjects;

public record ConfigParameterAllowedValue
{
    public string Value { get; }
    public string Label { get; }
    public ConfigParameterAllowedValue(string value, string label)
    {
        Value = value;
        Label = label;
    }
}
