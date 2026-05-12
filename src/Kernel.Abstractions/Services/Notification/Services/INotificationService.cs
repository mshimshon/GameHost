using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;

namespace GameHost.Kernel.Abstractions.Services.Notification.Services;

public interface INotificationService
{
    Task NotifyAsync(string message, NotificationSeverity severity);
    Task NotifyAsync(WebServiceException ex);
}
