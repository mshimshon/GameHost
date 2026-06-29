using GameHost.Kernel.Abstractions.Services.HostStateHookService;
using GameHost.Kernel.Services.HostStateHookService;
using LunaticPanel.Core.Abstraction.DependencyInjection;

namespace GameHost.Kernel;

public static class KernelServiceRegistration
{
    public static void AddKernelServices(this IPluginServiceCollection services)
    {
        services.AddScoped<IHostStateHookRegistry, HostStateHookRegistry>();
    }
}
