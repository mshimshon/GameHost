using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class FailedToCopyAssetsToServeLocationException : WebServiceException
{
    public FailedToCopyAssetsToServeLocationException(ICrazyReport crazyReport) :
        base(nameof(FailedToCopyAssetsToServeLocationException), "Failed to copy the assets from server control to serving location.", crazyReport)
    {
    }
}
