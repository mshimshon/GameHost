namespace GameHostCloud.Domain.Exceptions;

public class ManifestNameMustBeDefinedException : DomainException
{
    public ManifestNameMustBeDefinedException() :
        base(nameof(ManifestNameMustBeDefinedException), "The manifest name must be defined.")
    {
    }
}
