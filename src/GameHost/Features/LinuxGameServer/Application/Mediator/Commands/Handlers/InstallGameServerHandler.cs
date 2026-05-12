using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Commands.Handlers;

public class InstallGameServerHandler : IRequestHandler<InstallGameServerCommand>
{
    private readonly ILinuxGameServerService _linuxGameServerService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<InstallGameServerHandler> _crazyReport;
    private readonly IDispatcher _dispatcher;

    public InstallGameServerHandler(
        ILinuxGameServerService linuxGameServerService,
        INotificationService notificationService,
        ICrazyReport<InstallGameServerHandler> crazyReport, IDispatcher dispatcher)
    {
        _linuxGameServerService = linuxGameServerService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _dispatcher = dispatcher;
        _crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
    }
    public async Task Handle(InstallGameServerCommand request, CancellationToken ct)
        => await request
        .HandleWithData(request, Exec, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecAsync(ct);

    private Task Exec(InstallGameServerCommand request, CancellationToken ct)
        => _linuxGameServerService.PerformServerInstallation(request.Id, request.InstallerName, ct);

    private async Task OnFailure()
    {
        await Task.Delay(1000);
        await _dispatcher.Prepare<InstallGameServerStartFailedAction>().DispatchAsync();
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
