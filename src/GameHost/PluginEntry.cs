using GameHost.Core;
using GameHost.Features.Debugging.Web;
using GameHost.Features.Lifecycle;
using GameHost.Features.LinuxGameServer;
using GameHost.Features.Mods;
using GameHost.Features.Notification;
using GameHost.Features.SystemInfo;
using GameHost.Kernel;
using GameHost.Web.Middlewares.StatePulse;
using GameHost.Web.Pages.Hooks.UI.Components.ViewModels;
using GameHost.Web.Pages.ViewModels;
using LunaticPanel.Core;
using LunaticPanel.Core.Abstraction.Circuit;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Abstraction.Plugin;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Logging;
using MedihatR;
using MedihatR.Configuraions.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;
using StatePulse.Net.Configuration;

namespace GameHost;


public class PluginEntry : PluginBase
{
    private IConfiguration _configuration = default!;

    protected override void LoadConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;

    }
    private static Guid MasterId { get; set; }
    protected override void RegisterPluginServices(IServiceCollection services, CircuitIdentity circuit)
    {
        if (circuit.IsMaster) MasterId = circuit.CircuitId;

        services.AddStatePulseService<DispatchErrorMiddleware>();
        services.AddScoped<IHomeViewModel, HomeViewModel>();
        services.AddScoped<IWidgetMainPageMenuLinkViewModel, WidgetMainPageMenuLinkViewModel>();
        services.AddKernelServices();
        //services.AddScoped(sp => new PluginConfiguration(sp.GetRequiredService<IPluginConfiguration>(), sp.GetRequiredService<ICrazyReport>()));

        services.AddLogging();
        services.AddStatePulseServices(c =>
        {
            c.DispatchOrderBehavior = DispatchOrdering.ReducersFirst;
            c.PulseTrackingPerformance = PulseTrackingModel.BlazorServerSafe;
        });

        services.AddMedihaterServices(c =>
        {
            c.Performance = PipelinePerformance.DynamicMethods;
            c.NotificationFireMode = PipelineNotificationFireMode.FireAndForget;
            c.CachingMode = PipelineCachingMode.EagerCaching;
        });

        services.AddLifecycleFeatureServices(circuit.IsMaster);
        services.AddModFeatureServices(circuit.IsMaster);
        services.AddNotificationFeatureServices();
        services.AddLinuxGameServerFeatureServices(_crossCircuitSingletonProvider!, _configuration, circuit.IsMaster);
        services.AddSystemInfoFeatureServices(_configuration);
        services.AddDebuggingServices();
    }


    protected override async Task BeforeRuntimeStart(IPluginContextService pluginContext)
    {
        var sp = pluginContext.GetRequired<IServiceProvider>();
        Console.WriteLine($"{pluginContext.CircuitId} (Master? {pluginContext.IsMasterCircuit})");
        ICrazyReportCircuit crc = sp.GetRequiredService<ICrazyReportCircuit>();
        Console.WriteLine($"{crc.CircuitId} (Master? {pluginContext.IsMasterCircuit})");
        IEventBus eventBus = sp.GetRequiredService<IEventBus>();
        await eventBus.PublishDatalessAsync(PluginKeys.Events.OnBeforeRuntimeInitialization);

        await sp.RuntimeLifecycleInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeModInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeLinuxGameServerInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeSystemInfoFeatureInitializer(MasterId == pluginContext.CircuitId);

        await eventBus.PublishDatalessAsync(PluginKeys.Events.OnAfterRuntimeInitialization);
    }

    public override string[] GetMyPackageKeys() => Array.Empty<string>();
    public override void CheckFeatureDegradation(Func<string, bool> isBusAvailable)
    {

    }
}

