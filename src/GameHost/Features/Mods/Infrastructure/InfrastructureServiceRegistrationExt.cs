using GameHost.Features.Mods.Application;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Infrastructure.Services.ModList;
using LunaticPanel.Core.Abstraction.DependencyInjection;

namespace GameHost.Features.Mods.Infrastructure;

public static class InfrastructureServiceRegistrationExt
{
    public static void RegisterInfrastructureServices(this IPluginServiceCollection services)
    {
        services.RegisterApplicationServices();
        services.AddScoped<IModListService, ModListService>();
    }
}
