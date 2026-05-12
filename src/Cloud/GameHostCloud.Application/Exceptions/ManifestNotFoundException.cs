namespace GameHostCloud.Application.Exceptions;

public class ManifestNotFoundException : ApplicationException
{
    public ManifestNotFoundException() :
        base(nameof(ManifestNotFoundException), "The manifest was not found.")
    {
    }
}
