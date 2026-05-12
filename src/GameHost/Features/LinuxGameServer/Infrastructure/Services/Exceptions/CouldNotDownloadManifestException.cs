using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class CouldNotDownloadManifestException : WebServiceException
{
    public CouldNotDownloadManifestException(ICrazyReport crazyReport) :
        base(nameof(CouldNotDownloadManifestException), "Couldn't download the manifest of available game server.", crazyReport)
    {
    }
}
