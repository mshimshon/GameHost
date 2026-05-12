using GameHost.Kernel.Abstractions.Exceptions;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Git.Exceptions;

public class GitPackageNotInstalledException : WebServiceException
{
    public GitPackageNotInstalledException() :
        base(nameof(GitPackageNotInstalledException), "Git is not installed on target system.")
    {
    }

}
