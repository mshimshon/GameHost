using GameHostCloud.Domain.Exceptions;

namespace GameHostCloud.Domain.ValueObjects;

public sealed record ManifestId
{
    public string Id { get; }

    public ManifestId(string id)
    {
        id.NullWhiteSpaceThrow<ManifestIdMustBeDefinedException>();
        Id = id;

    }

}
