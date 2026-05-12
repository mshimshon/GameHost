using GameHost.Core.Features;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Mediator.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Kernel.Abstractions.Mediator;

public abstract class HandlerBase
{
    protected readonly INotificationService _notificationService;
    private readonly ICrazyReport _crazyReport;

    protected HandlerBase(INotificationService notificationService, ICrazyReport crazyReport)
    {
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(KernelKeys.MODULE_NAME);
    }

    protected virtual async Task ExecAndHandleExceptions(Func<Task> exec, Action<WebServiceException>? onError = default)
    {
        try
        {
            await exec();
        }
        catch (CommandFailedException ex)
        {
            await _notificationService.NotifyAsync(ex.Message, NotificationSeverity.Error);
            _crazyReport.ReportError(ex.Message);
            _crazyReport.ReportError(ex.StdErr);
            _crazyReport.ReportError(ex.StdOut);
            if (onError != default)
                onError.Invoke(new UnknownWebServiceException(ex));
        }
        catch (WebServiceException ex)
        {
            await _notificationService.NotifyAsync(ex.Message, NotificationSeverity.Error);
            if (ex.Origin != default)
                _crazyReport.ReportErrorException(ex.Message, ex.Origin, _crazyReport);
            else
                _crazyReport.ReportError(ex.Message);
            if (onError != default)
                onError.Invoke(ex);
        }
        catch (Exception ex)
        {
            await _notificationService.NotifyAsync("Unknown Error, Please contact admins if persistent.", NotificationSeverity.Error); // TODO: Localize
            _crazyReport.ReportErrorException(ex.Message, ex, _crazyReport);
            if (onError != default)
                onError.Invoke(new UnknownWebServiceException(ex)); // TODO: Localize
        }
    }


    protected async Task<TResult> ExecAndHandleExceptions<TResult>(Func<Task<TResult>> exec, Func<TResult> onError)
    {
        TResult? result = default;
        await ExecAndHandleExceptions(async () =>
        {
            result = await exec();
        });
        return result ?? onError();
    }
}
