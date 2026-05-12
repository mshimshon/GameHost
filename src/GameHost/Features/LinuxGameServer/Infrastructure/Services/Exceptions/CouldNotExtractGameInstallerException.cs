using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class CouldNotExtractGameInstallerException : WebServiceException
{
    public CouldNotExtractGameInstallerException(ICrazyReport crazyReport) :
        base(nameof(CouldNotExtractGameInstallerException), "Failed to extract the installer.", crazyReport)
    {
    }
}
