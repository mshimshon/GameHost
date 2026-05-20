using GameHost.Features.SystemInfo.Application.Services;
using GameHost.Features.SystemInfo.Infrastructure;
using GameHost.Features.SystemInfo.Infrastructure.Configurations;
using GameHost.Features.SystemInfo.Infrastructure.Services;
using GameHost.Features.SystemInfo.Web.Components.ViewModels;
using GameHost.Features.SystemInfo.Web.Hooks.Events.Scheduled;
using GameHost.Features.SystemInfo.Web.Hooks.UI.Components.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.SystemInfo;

public static class SystemInfoServiceExt
{
    public static void AddSystemInfoFeatureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var linuxSysInfoConfig =
        configuration.GetSection("SystemInfo").GetSection("Linux")?.Get<LinuxSystemInfoConfiguration>() ??
        new LinuxSystemInfoConfiguration();
        services.AddInfrastructureServices();
        services.AddScoped<ISystemInfoService, LinuxSystemInfoService>();

        services.AddScoped<LinuxSystemInfoConfiguration>((sp) => linuxSysInfoConfig);

        services.AddScoped<ISystemResourcesStatusViewModel, SystemResourcesStatusViewModel>();
        services.AddScoped<IWidgetSystemInfoViewModel, WidgetSystemInfoViewModel>();

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
