using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class ManifestRepositoryNotYetReadyException : WebServiceException
{
    public ManifestRepositoryNotYetReadyException(ICrazyReport crazyReport) :
        base(nameof(ManifestRepositoryNotYetReadyException), "The manifest of game server is not ready.", crazyReport)
    {
    }
}
