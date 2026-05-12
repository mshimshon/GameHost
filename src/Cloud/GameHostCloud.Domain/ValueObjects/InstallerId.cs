using GameHostCloud.Domain.Exceptions;

namespace GameHostCloud.Domain.ValueObjects;

public sealed record InstallerId
{
    public string Filename { get; }
    public string Source { get; }

    public InstallerId(string source, string filename)
    {
        source.NullWhiteSpaceThrow<InstallerSourceRequiredException>().PatternMatch<InstallerSourceRequiredException>(@"^https://[A-Za-z0-9.-]+/$");
        Source = source;
        filename.NullWhiteSpaceThrow<InstallerNameRequiredException>().PatternMatch<InstallerNameRequiredException>(@"^[A-Za-z0-9._-]+\.tar\.gz$");
        Filename = filename;
    }

}
