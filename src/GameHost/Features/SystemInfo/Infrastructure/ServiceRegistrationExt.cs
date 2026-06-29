using GameHost.Features.SystemInfo.Application;
using LunaticPanel.Core.Abstraction.DependencyInjection;

namespace GameHost.Features.SystemInfo.Infrastructure;

public static class ServiceRegistrationExt
{
    public static void AddInfrastructureServices(this IPluginServiceCollection services)
    {
        services.AddApplicationServices();
    }

}
