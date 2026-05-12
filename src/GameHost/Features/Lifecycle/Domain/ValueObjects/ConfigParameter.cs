using GameHost.Features.Lifecycle.Domain.Enums;

namespace GameHost.Features.Lifecycle.Domain.ValueObjects;

public sealed record ConfigParameter
{
    public string Key { get; }
    public ConfigParameterType ConfigParameterType { get; }
    public ConfigParameter(string key, ConfigParameterType startupParameterType)
    {
        Key = key;
        ConfigParameterType = startupParameterType;
    }
}
