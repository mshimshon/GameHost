using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class RequestedManifestNotFoundException : WebServiceException
{
    public RequestedManifestNotFoundException(string id, ICrazyReport crazyReport) :
        base(nameof(RequestedManifestNotFoundException), $"{id} was not found in the manifest.", crazyReport)
    {
    }
}
