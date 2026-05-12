using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Exceptions;

public class FailedToGetServerStatusException : WebServiceException
{
    public FailedToGetServerStatusException(ICrazyReport crazyReport) :
        base(nameof(FailedToGetServerStatusException), "Failed to fetch server status.", crazyReport)
    {
    }

    public FailedToGetServerStatusException(Exception origin, ICrazyReport crazyReport) :
        base(nameof(FailedToGetServerStatusException), "Failed to fetch server status.", origin, crazyReport)
    {
    }
}
