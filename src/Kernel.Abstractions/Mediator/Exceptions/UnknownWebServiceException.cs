using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Kernel.Abstractions.Mediator.Exceptions;

public class UnknownWebServiceException : WebServiceException
{
    private const string MESSAGE = "Unknown Web Service exception happened.";
    public UnknownWebServiceException() :
        base(nameof(UnknownWebServiceException), MESSAGE)
    {
    }

    public UnknownWebServiceException(Exception origin) :
        base(nameof(UnknownWebServiceException), MESSAGE, origin)
    {
    }

    public UnknownWebServiceException(ICrazyReport crazyReport) :
        base(nameof(UnknownWebServiceException), MESSAGE, crazyReport)
    {
    }

    public UnknownWebServiceException(Exception origin, ICrazyReport crazyReport) :
        base(nameof(UnknownWebServiceException), MESSAGE, origin, crazyReport)
    {
    }
}
