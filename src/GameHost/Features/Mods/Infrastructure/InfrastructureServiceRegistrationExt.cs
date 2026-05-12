using GameHost.Features.Mods.Application;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Infrastructure.Services;
using GameHost.Features.Mods.Infrastructure.Services.Contracts.Mapping;
using GameHost.Kernel.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.Mods.Infrastructure;

public static class InfrastructureServiceRegistrationExt
{
    public static void RegisterModInfrastructureServices(this IServiceCollection services)
    {
        services.RegisterModApplicationServices();
        services.AddScoped<IModListService, ModListService>();
        services.AddCoreMapHandler<ModEntityToModResponse>();
        services.AddCoreMapHandler<ModListEntityToModListResponse>();
        services.AddCoreMapHandler<ModListResponseToModListEntity>();
        services.AddCoreMapHandler<ModResponseToModEntity>();
        services.AddCoreMapHandler<GameInfoResponseToGameInfoEntity>();
        services.AddCoreMapHandler<ModFeatureResponseToModFeatureEntity>();

    }
}
