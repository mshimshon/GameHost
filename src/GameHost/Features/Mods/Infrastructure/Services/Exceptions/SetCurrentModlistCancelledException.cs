using GameHost.Kernel.Abstractions.Exceptions;

namespace GameHost.Features.Mods.Infrastructure.Services.Exceptions;

public class SetCurrentModlistCancelledException : WebServiceException
{
    public SetCurrentModlistCancelledException() :
        base(nameof(SetCurrentModlistCancelledException), "The action of setting current modlist has been cancelled.")
    {
    }
}
