using GameHost.Kernel.Abstractions.Exceptions;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Git.Exceptions;

public class GitCloneFailedException : WebServiceException
{
    public GitCloneFailedException() :
        base(nameof(GitCloneFailedException), "Couldn't clone repository using git, check logs.")
    {
    }
}
