using GameHostCloud.Domain.Exceptions;

namespace GameHostCloud.Domain.ValueObjects;

public sealed record ManifestMetadata
{
    public ManifestMetadata(string name)
    {
        Name = name;
        name.NullWhiteSpaceThrow<ManifestIdMustBeDefinedException>();
        Name = name;
    }

    public ManifestMetadata(string name, string icon) : this(name)
    {
        if (string.IsNullOrWhiteSpace(icon)) return;
        icon.PatternMatch<ManifestIconMustBeValidException>(@"^[A-Za-z0-9+/]+={0,2}$");
        Icon = icon;
    }

    public string Name { get; }
    public string? Icon { get; }
}
