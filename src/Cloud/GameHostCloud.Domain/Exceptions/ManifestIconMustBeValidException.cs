namespace GameHostCloud.Domain.Exceptions;

public class ManifestIconMustBeValidException : DomainException
{
    public ManifestIconMustBeValidException() :
        base(nameof(ManifestIconMustBeValidException), "The icon must be a valid b64 string.")
    {
    }
}
