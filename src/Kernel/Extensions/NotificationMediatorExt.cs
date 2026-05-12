using GameHost.Kernel.Abstractions.Mediator.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Kernel.Extensions;

public static class NotificationMediatorExt
{
    public static async Task HandleUnknownException(this INotificationService service, ICrazyReport crazyReport, Exception ex)
    {
        var webEx = new UnknownWebServiceException(ex, crazyReport);
        await service.NotifyAsync(webEx);
    }
}
