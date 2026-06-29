using GameHost.Features.Notification.Application.Pulses.Actions;
using GameHost.Features.Notification.Application.Pulses.Effects;
using GameHost.Features.Notification.Infrastructure.Services;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Abstraction.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Features.Notification;

internal static class NotificationServiceExt
{
    public static void AddNotificationFeatureServices(this IPluginServiceCollection services)
    {
        services.Services.AddStatePulseService<SendToastNotificationAction>();
        services.Services.AddStatePulseService<SendToastNotificationEffect>();
        services.AddScoped<INotificationService, ToastNotificationService>();

    }
}
