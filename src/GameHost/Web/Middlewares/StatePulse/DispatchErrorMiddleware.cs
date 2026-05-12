using GameHost.Core.Features;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Web.Middlewares.StatePulse;

internal class DispatchErrorMiddleware : IDispatcherMiddleware
{
    private readonly ICrazyReport _crazyReport;

    public DispatchErrorMiddleware(ICrazyReport<DispatchErrorMiddleware> crazyReport, INotificationService notificationService)
    {
        _crazyReport = crazyReport;
        _crazyReport.SetModule(KernelKeys.MODULE_NAME);
    }
    public Task AfterDispatch(object action) => Task.CompletedTask;
    public Task BeforeDispatch(object action) => Task.CompletedTask;
    public Task OnDispatchFailure(Exception ex, object action)
    {
        _crazyReport.ReportError(":[{0}]: {1} ", action.GetType(), ex.Message);
        return Task.CompletedTask;
    }
}
