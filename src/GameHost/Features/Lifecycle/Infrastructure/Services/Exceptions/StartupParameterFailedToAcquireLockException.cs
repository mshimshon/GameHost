using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Exceptions;

public class StartupParameterFailedToAcquireLockException : WebServiceException
{
    public StartupParameterFailedToAcquireLockException(Exception origin, ICrazyReport crazyReport) :
        base(nameof(StartupParameterFailedToAcquireLockException), "Someone else is already saving start up parameter, try again.", origin, crazyReport)
    {
    }
}
