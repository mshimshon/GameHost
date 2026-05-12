using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Exceptions;

public class GameServerNotInstalledException : WebServiceException
{
    public GameServerNotInstalledException(ICrazyReport crazyReport) :
        base(nameof(GameServerNotInstalledException), "It looks like the game server is not installed.", crazyReport) //TODO: Localize
    {
    }
}
