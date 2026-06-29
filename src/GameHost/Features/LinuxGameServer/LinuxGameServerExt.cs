using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using GameHost.Features.LinuxGameServer.Infrastructure;
using GameHost.Features.LinuxGameServer.Infrastructure.Configuration;
using GameHost.Features.LinuxGameServer.Web.Components.ViewModels;
using GameHost.Features.LinuxGameServer.Web.Hooks.UI.Components.ViewModels;
using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer;

public static class LinuxGameServerExt
{
    public static void AddLinuxGameServerFeatureServices(this IPluginServiceCollection services,
        IServiceProvider singletonCrossCircuitSp,
        IConfiguration configuration,
        bool isMaster)
    {
        var config =
            configuration.GetSection("LinuxGameServer")?.Get<LinuxGameServerConfiguration>() ??
            new LinuxGameServerConfiguration();
        services.AddInfrastructureServices(singletonCrossCircuitSp, configuration, isMaster);
        services.AddScoped(sp => config);

        services.AddScoped<ISetupProcessViewModel, SetupProcessViewModel>();
        services.AddScoped<IWidgetServerSetupViewModel, WidgetServerSetupViewModel>();



        if (isMaster)
        {
            services.Services.AddMasterStateFileWatcherService<UpdateInstalledGameServerAction>(
                c => c.GetConfigBase(LinuxGameServerKeys.MODULE_NAME),
                LinuxGameServerKeys.SERVER_INSTALL_STATE_FILE,
                [FileWatchEvents.Any]);

            services.Services.AddMasterStateFileWatcherService<UpdateProgressStateFromDiskAction>(
                c => c.GetConfigBase(LinuxGameServerKeys.MODULE_NAME),
                LinuxGameServerKeys.SERVER_INSTALL_PROGRESS_FILE,
                [FileWatchEvents.Any]);
        }


    }

    public static async Task RuntimeLinuxGameServerInitializer(this IServiceProvider serviceProvider, bool isMaster)
    {
        if (isMaster)
        {
            var state = serviceProvider.GetRequiredService<IStateAccessor<InstallationState>>();
            await serviceProvider.LoadInstallationState();
            await serviceProvider.SetupAvailableGameServer();
            serviceProvider.LoadWatcher<UpdateInstalledGameServerAction>();
            if (state.State.IsInstallationCompleted) return;
            serviceProvider.LoadWatcher<UpdateProgressStateFromDiskAction>();
        }


    }

    private static async Task LoadInstallationState(this IServiceProvider serviceProvider)
    {
        var state = serviceProvider.GetRequiredService<IStateAccessor<InstallationState>>();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        await dispatcher.Prepare<UpdateInstalledGameServerAction>().Await().DispatchAsync();
        if (state.State.IsInstallationCompleted) return;
        await dispatcher.Prepare<UpdateProgressStateFromDiskAction>().DispatchAsync();
    }
    private static async Task SetupAvailableGameServer(this IServiceProvider serviceProvider)
    {
        var installationState = serviceProvider.GetRequiredService<IStateAccessor<InstallationState>>();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        await dispatcher.Prepare<PopulateAvailableGamesForInstallAction>().Await().DispatchAsync();
    }
}
