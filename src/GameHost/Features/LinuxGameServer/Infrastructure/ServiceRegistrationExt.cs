using GameHost.Features.LinuxGameServer.Application;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Features.LinuxGameServer.Infrastructure.Services;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GameHost.Features.LinuxGameServer.Infrastructure;

public static class ServiceRegistrationExt
{
    public static void AddInfrastructureServices(this IPluginServiceCollection services, IServiceProvider singletonCrossCircuitSp,
        IConfiguration configuration,
        bool isMaster)
    {
        services.AddApplicationServices(singletonCrossCircuitSp, configuration, isMaster);
        services.AddScoped<ILinuxGameServerService, LinuxGameServerService>();
    }
}
