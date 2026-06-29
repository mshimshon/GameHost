using GameHost.Features.Debugging.Web.Pages;
using LunaticPanel.Core.Abstraction.DependencyInjection;

namespace GameHost.Features.Debugging.Web;

public static class ServiceRegisterDebuggingExt
{
    public static void AddDebuggingServices(this IPluginServiceCollection services)
    {
        services.AddScoped<IDebuggingViewModel, DebuggingViewModel>();
    }
}
