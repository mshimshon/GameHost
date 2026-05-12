using GameHost.Kernel.Abstractions.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;

public class ManifestRepositoryNotFoundException : WebServiceException
{
    public ManifestRepositoryNotFoundException(ICrazyReport crazyReport) :
        base(nameof(ManifestRepositoryNotFoundException), "The Manifest file was not found.", crazyReport)
    {
    }
}
