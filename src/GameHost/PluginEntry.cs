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
using LunaticPanel.Core.Abstraction.DependencyInjection;
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
    protected override void RegisterPluginServices(IPluginServiceCollection services, CircuitIdentity circuit)
    {
        if (circuit.IsMaster) MasterId = circuit.CircuitId;

        services.Services.AddStatePulseService<DispatchErrorMiddleware>();
        services.AddScoped<IHomeViewModel, HomeViewModel>();
        services.AddScoped<IWidgetMainPageMenuLinkViewModel, WidgetMainPageMenuLinkViewModel>();
        //services.AddScoped(sp => new PluginConfiguration(sp.GetRequiredService<IPluginConfiguration>(), sp.GetRequiredService<ICrazyReport>()));

        services.Services.AddLogging();
        services.Services.AddStatePulseServices(c =>
        {
            c.DispatchOrderBehavior = DispatchOrdering.ReducersFirst;
            c.PulseTrackingPerformance = PulseTrackingModel.BlazorServerSafe;
        });

        services.Services.AddMedihaterServices(c =>
        {
            c.Performance = PipelinePerformance.DynamicMethods;
            c.NotificationFireMode = PipelineNotificationFireMode.FireAndForget;
            c.CachingMode = PipelineCachingMode.EagerCaching;
        });

        services.AddKernelServices();
        services.AddLifecycleFeatureServices(circuit.IsMaster);
        services.AddModFeatureServices(circuit.IsMaster);
        services.AddNotificationFeatureServices();
        services.AddLinuxGameServerFeatureServices(_crossCircuitSingletonProvider!, _configuration, circuit.IsMaster);
        services.AddSystemInfoFeatureServices(_configuration);
        services.AddDebuggingServices();

        //if (_statePulseStatesRedirectionSingleton == default)
        //{
        //    _statePulseStatesRedirectionSingleton = new ServiceCollection();
        //    _statePulseStatesSingleton = new();
        //    foreach (var d in services)
        //    {
        //        if (d.ServiceType.IsGenericTypeDefinition)
        //            continue;

        //        if (d.Lifetime != ServiceLifetime.Singleton)
        //            continue;

        //        if (!d.ServiceType.IsGenericType ||
        //            d.ServiceType.GetGenericTypeDefinition() != typeof(IStateAccessor<>))
        //            continue;
        //        _statePulseStatesSingleton.Add(d);
        //        _statePulseStatesRedirectionSingleton.AddSingleton(d.ServiceType, sp => _crossCircuitSingletonProvider!.GetRequiredService(d.ServiceType));
        //    }
        //}
        //if (_statePulseStatesRedirectionSingleton != default)
        //    foreach (var item in _statePulseStatesRedirectionSingleton)
        //        services.Add(item);
    }


    protected override async Task BeforeRuntimeStart(IPluginContextService pluginContext)
    {
        var sp = pluginContext.GetRequired<IServiceProvider>();
        Console.WriteLine($"{pluginContext.CircuitId} (Master? {pluginContext.IsMasterCircuit})");
        ICrazyReportCircuit crc = sp.GetRequiredService<ICrazyReportCircuit>();
        Console.WriteLine($"{crc.CircuitId} (Master? {pluginContext.IsMasterCircuit})");
        IEventBus eventBus = sp.GetRequiredService<IEventBus>();
        await eventBus.PublishDatalessAsync(GameHostKeys.Events.ON_BEFORE_RUNTIME_INITIALIZATION);

        await sp.RuntimeLifecycleInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeModInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeLinuxGameServerInitializer(MasterId == pluginContext.CircuitId);
        await sp.RuntimeSystemInfoFeatureInitializer(MasterId == pluginContext.CircuitId);

        await eventBus.PublishDatalessAsync(GameHostKeys.Events.ON_AFTER_RUNTIME_INITIALIZATION);
    }

    public override string[] GetMyPackageKeys() => typeof(PluginKeys).Assembly.ScanKeyPackageForKeys();
    public override void CheckFeatureDegradation(Func<string, bool> isBusAvailable)
    {

    }
}

