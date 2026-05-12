using GameHost.Features.SystemInfo.Application.CQRS.Queries;
using GameHost.Features.SystemInfo.Application.CQRS.Queries.Handlers;
using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Application.Pulses.Effects;
using GameHost.Features.SystemInfo.Application.Pulses.Reducers;
using GameHost.Features.SystemInfo.Application.Pulses.Reducers.Middlewares;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using GameHost.Features.SystemInfo.Application.Services;
using GameHost.Features.SystemInfo.Domain.Entites;
using GameHost.Features.SystemInfo.Infrastructure.Configurations;
using GameHost.Features.SystemInfo.Infrastructure.Services;
using GameHost.Features.SystemInfo.Web.Components.ViewModels;
using GameHost.Features.SystemInfo.Web.Hooks.Events.Scheduled;
using GameHost.Features.SystemInfo.Web.Hooks.UI.Components.ViewModels;
using MedihatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo;

public static class SystemInfoServiceExt
{
    public static void AddSystemInfoFeatureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var linuxSysInfoConfig =
        configuration.GetSection("SystemInfo").GetSection("Linux")?.Get<LinuxSystemInfoConfiguration>() ??
        new LinuxSystemInfoConfiguration();

        services.AddScoped<ISystemInfoService, LinuxSystemInfoService>();

        services.AddScoped<LinuxSystemInfoConfiguration>((sp) => linuxSysInfoConfig);

        services.AddScoped<ISystemResourcesStatusViewModel, SystemResourcesStatusViewModel>();
        services.AddScoped<IWidgetSystemInfoViewModel, WidgetSystemInfoViewModel>();
        services.AddStatePulseService<SystemInfoUpdateAction>();
        services.AddStatePulseService<SystemInfoUpdatedAction>();
        services.AddStatePulseService<SystemInfoUpdateEffect>();
        services.AddStatePulseService<ServerSystemInfoUpdatedReducer>();
        services.AddStatePulseService<SystemInfoState>();
        services.AddStatePulseService<OnServerInfoUpdateMiddleware>();
        services.AddMedihaterRequestHandler<GetSystemInfoQuery, GetSystemInfoHandler, SystemInfoEntity?>();
        services.AddSingleton<PeriodicSystemInfoUpdate>();
    }
    public static async Task RuntimeSystemInfoFeatureInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {
        if (isMaster)
        {
            // TODO: CLEAN UP 
            //var periodicResourceUpdater = serviceProvider.GetRequiredService<PeriodicSystemInfoUpdate>();
            //_ = periodicResourceUpdater.StartAsync();
        }

    }
}
