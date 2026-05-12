using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.Lifecycle.Infrastructure.Services.Exceptions;

public class FailedToExecuteStartCommandException : WebServiceException
{
    public FailedToExecuteStartCommandException(ICrazyReport crazyReport) :
        base(nameof(FailedToExecuteStartCommandException), "The server console seems to have failed to execute the start command.", crazyReport)
    {
    }
}
