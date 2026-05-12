using GameHost.Features.Mods.Domain.Exceptions;

namespace GameHost.Features.Mods.Domain.ValueObjects;

public sealed record PartOutput
{
    public string? Suffix { get; }
    public string? Prefix { get; }
    public string? Wrapper { get; }
    public string? Separator { get; }
    public PartOutput(string? suffix, string? prefix, string? wrapper, string? separator)
    {
        Suffix = suffix;
        Prefix = prefix;
        Wrapper = wrapper;
        Separator = separator;
        bool isEmpty = string.IsNullOrEmpty(suffix) && string.IsNullOrEmpty(prefix) && string.IsNullOrEmpty(wrapper) && string.IsNullOrEmpty(separator);
        if (isEmpty)
            throw new EmptyException<PartOutput>(string.Join(',', [nameof(Suffix), nameof(Prefix), nameof(Wrapper), nameof(Separator)]));
    }
}
