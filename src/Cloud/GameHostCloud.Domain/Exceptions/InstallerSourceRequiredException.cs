namespace GameHostCloud.Domain.Exceptions;

public class InstallerSourceRequiredException : DomainException
{
    public InstallerSourceRequiredException() :
        base(nameof(InstallerSourceRequiredException), "The source of the installer is required in format https://whatever/")
    {
    }
}
