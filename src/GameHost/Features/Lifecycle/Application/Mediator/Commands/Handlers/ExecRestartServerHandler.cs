using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Mediator.Commands.Handlers;

public class ExecRestartServerHandler : IRequestHandler<ExecRestartServerCommand>
{
    private readonly ILifecycleServices _lifecycleServices;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<ExecRestartServerHandler> _crazyReport;
    private readonly IDispatcher _dispatcher;

    public ExecRestartServerHandler(ILifecycleServices lifecycleServices, INotificationService notificationService,
        ICrazyReport<ExecRestartServerHandler> crazyReport, IDispatcher dispatcher)
    {
        _lifecycleServices = lifecycleServices;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _dispatcher = dispatcher;
        _crazyReport.SetModule(LifecycleKeys.MODULE_NAME);
    }
    public Task Handle(ExecRestartServerCommand request, CancellationToken ct)
        => request
            .HandleWithData(request, Exec, OnFailure)
            .HandleExceptionFor<WebServiceException>(OnFailure)
            .ExecAsync(ct);

    private Task Exec(ExecRestartServerCommand request, CancellationToken ct)
        => _lifecycleServices.ServerRestartAsync(ct);

    private async Task OnFailure()
    {
        await _dispatcher.Prepare<TransitionDoneAction>().DispatchAsync();
    }


    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
        await OnFailure();
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
        await OnFailure();
    }
}
