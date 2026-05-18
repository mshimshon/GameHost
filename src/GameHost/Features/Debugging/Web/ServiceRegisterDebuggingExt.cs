using GameHost.Features.Debugging.Web.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace GameHost.Features.Debugging.Web;

public static class ServiceRegisterDebuggingExt
{
    public static void AddDebuggingServices(this IServiceCollection services)
    {
        services.AddScoped<IDebuggingViewModel, DebuggingViewModel>();
    }
}
