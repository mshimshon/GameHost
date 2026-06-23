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
    private IServiceCollection? _statePulseStatesRedirectionSingleton;
    private List<ServiceDescriptor>? _statePulseStatesSingleton;
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
        //services.AddTransient(typeof(ICrazyReport<>), typeof(CrazyReport<>));
        //services.AddTransient<ICrazyReport, CrazyReport>();
        // Make Singleton State cross circuit
        if (_statePulseStatesRedirectionSingleton == default)
        {
            _statePulseStatesRedirectionSingleton = new ServiceCollection();
            _statePulseStatesSingleton = new();
            foreach (var d in services)
            {
                if (d.ServiceType.IsGenericTypeDefinition)
                    continue;

                if (d.Lifetime != ServiceLifetime.Singleton)
                    continue;

                if (!d.ServiceType.IsGenericType ||
                    d.ServiceType.GetGenericTypeDefinition() != typeof(IStateAccessor<>))
                    continue;
                _statePulseStatesSingleton.Add(d);
                _statePulseStatesRedirectionSingleton.AddSingleton(d.ServiceType, sp => _crossCircuitSingletonProvider!.GetRequiredService(d.ServiceType));
            }
        }
        if (_statePulseStatesRedirectionSingleton != default)
            foreach (var item in _statePulseStatesRedirectionSingleton)
                services.Add(item);
    }
    protected override void RegisterPluginSingletonServices(IServiceCollection services, CircuitIdentity circuit)
    {
        if (_statePulseStatesSingleton != default)
        {
            foreach (var d in _statePulseStatesSingleton)
                services.Add(d);
        }

    }

    protected override async Task BeforeRuntimeStart(IPluginContextService pluginContext)
    {
        var sp = pluginContext.GetRequired<IServiceProvider>();
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

