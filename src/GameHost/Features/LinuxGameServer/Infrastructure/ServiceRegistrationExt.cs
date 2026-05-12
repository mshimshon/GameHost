using GameHost.Features.LinuxGameServer.Application;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Features.LinuxGameServer.Infrastructure.Services;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Contracts.Responses.Mapping;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Git;
using GameHost.Kernel.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.LinuxGameServer.Infrastructure;

public static class ServiceRegistrationExt
{
    public static void AddInfrastructureServices(this IServiceCollection services, IServiceProvider singletonCrossCircuitSp,
        IConfiguration configuration,
        bool isMaster)
    {
        services.AddApplicationServices(singletonCrossCircuitSp, configuration, isMaster);
        services.AddScoped<IGitService, GitService>();
        services.AddScoped<ILinuxGameServerService, LinuxGameServerService>();
        services.AddCoreMapHandler<GameManifestResponseToGameManifestEntity>();
    }
}
