using GameHost.Kernel.Abstractions.Exceptions;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Git.Exceptions;

public class GitPermissionFixFailedException : WebServiceException
{
    public GitPermissionFixFailedException() :
        base(nameof(GitPermissionFixFailedException), "Couldn't fix permission on the git target")
    {
    }
}
