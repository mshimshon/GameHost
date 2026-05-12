using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.Mods.Infrastructure.Services.Exceptions;

public class CouldNotSerializeIdForCurrentModlistException : WebServiceException
{
    public CouldNotSerializeIdForCurrentModlistException(Exception origin, ICrazyReport crazyReport) :
        base(nameof(CouldNotSerializeIdForCurrentModlistException), "The id for setting the current failed to serialize.", origin, crazyReport)
    {
    }
}
