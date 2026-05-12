using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services;

public class GameManifestFileFailedToDeserializeException : WebServiceException
{
    public GameManifestFileFailedToDeserializeException(Exception origin, ICrazyReport crazyReport) :
        base(nameof(GameManifestFileFailedToDeserializeException), "Couldn't read the manifest file for the available game list.", origin, crazyReport)
    {
    }
}
