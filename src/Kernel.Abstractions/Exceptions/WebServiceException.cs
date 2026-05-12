using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Kernel.Abstractions.Exceptions;


public class WebServiceException : Exception
{

    public WebServiceException(string code, string message) : base(message)
    {
        Code = code;

    }
    public WebServiceException(string code, string message, Exception origin) : this(code, message)
    {
        Origin = origin;
    }

    public WebServiceException(string code, string message, ICrazyReport crazyReport) : this(code, message)
    {
        crazyReport.ReportErrorException(message!, this);
    }
    public WebServiceException(string code, string message, Exception origin, ICrazyReport crazyReport) : this(code, message)
    {
        crazyReport.ReportErrorException(message!, origin);
    }

    public Exception? Origin { get; }
    public string Code { get; }
}
