namespace GameHost.Features.Lifecycle.Domain.ValueObjects;

public sealed record ConfigInfoKey
{
    public ConfigInfoKey(string key)
    {
        Key = key;
    }

    public string Key { get; }
}
