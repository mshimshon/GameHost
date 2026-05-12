namespace GameHostCloud.Domain.Exceptions;

public class InstallerNameRequiredException : DomainException
{
    public InstallerNameRequiredException() :
        base(nameof(InstallerNameRequiredException), "The installer filename is required with format somename.tar.gz")
    {
    }
}
