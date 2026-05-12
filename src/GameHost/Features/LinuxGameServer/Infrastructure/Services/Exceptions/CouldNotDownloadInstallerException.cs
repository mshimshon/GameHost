using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class CouldNotDownloadInstallerException : WebServiceException
{
    public CouldNotDownloadInstallerException(ICrazyReport crazyReport) :
        base(nameof(CouldNotDownloadInstallerException), "Couldn't download the installer of game server.", crazyReport)
    {
    }
}
