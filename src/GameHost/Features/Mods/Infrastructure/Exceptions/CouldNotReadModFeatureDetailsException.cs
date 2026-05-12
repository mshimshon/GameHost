using GameHost.Kernel.Abstractions.Exceptions;

namespace GameHost.Features.Mods.Infrastructure.Exceptions;

public class CouldNotReadModFeatureDetailsException : WebServiceException
{
    public CouldNotReadModFeatureDetailsException() :
        base(nameof(CouldNotReadModFeatureDetailsException), "Could not read the mod feature details.")
    {
    }
}
