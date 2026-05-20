using GameHost.Features.SystemInfo.Application;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.SystemInfo.Infrastructure;

public static class ServiceRegistrationExt
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddApplicationServices();
    }

}
