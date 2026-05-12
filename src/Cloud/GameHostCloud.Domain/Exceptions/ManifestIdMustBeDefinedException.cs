namespace GameHostCloud.Domain.Exceptions;

public class ManifestIdMustBeDefinedException : DomainException
{
    public ManifestIdMustBeDefinedException() :
        base(nameof(ManifestIdMustBeDefinedException), "The manifest id is required.")
    {
    }
}
