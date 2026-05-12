using GameHost.Kernel.Abstractions.Services.HostStateHookService;
using GameHost.Kernel.Services.HostStateHookService;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Kernel;

public static class KernelServiceRegistration
{
    public static void AddKernelServices(this IServiceCollection services)
    {
        services.AddScoped<IHostStateHookRegistry, HostStateHookRegistry>();
    }
}
