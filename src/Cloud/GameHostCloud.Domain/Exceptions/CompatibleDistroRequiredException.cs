namespace GameHostCloud.Domain.Exceptions;

public class CompatibleDistroRequiredException : DomainException
{
    public CompatibleDistroRequiredException() :
        base(nameof(CompatibleDistroRequiredException), "Compatible Distro collection is required.")
    {
    }
}
