using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class CouldNotInitializeInstallerException : WebServiceException
{
    public CouldNotInitializeInstallerException(ICrazyReport crazyReport) :
        base(nameof(CouldNotInitializeInstallerException), "Could not initialize the installer.", crazyReport)
    {
    }
}
