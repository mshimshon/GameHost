using GameHost.Features.Mods.Application;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Infrastructure.Services.ModList;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.Mods.Infrastructure;

public static class InfrastructureServiceRegistrationExt
{
    public static void RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.RegisterApplicationServices();
        services.AddScoped<IModListService, ModListService>();
    }
}
