using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using GameHost.Features.LinuxGameServer.Application.Payloads.Responses.Mapping;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Features.LinuxGameServer.Domain.Entities;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries.Handlers;

internal class GetInstallationProgressHandler : IRequestHandler<GetInstallationProgressQuery, GameServerInstallProgressResponse?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetInstallationProgressHandler> _crazyReport;

    public GetInstallationProgressHandler(ILinuxGameServerService linuxGameServerService,
        INotificationService notificationService, ICrazyReport<GetInstallationProgressHandler> crazyReport)

    {
        _linuxGameServerService = linuxGameServerService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
    }

    public async Task<GameServerInstallProgressResponse?> Handle(GetInstallationProgressQuery request, CancellationToken cancellationToken)
        => await request
        .Handle(_linuxGameServerService.GetInstallationProgress, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecOrDefaultAsync<GameServerInstallProgressEntity, GameServerInstallProgressResponse>((e) => e.MapToApplication(), cancellationToken);

    private Task OnFailure(Exception ex) => _notificationService.HandleUnknownException(_crazyReport, ex);

    private Task OnFailure(WebServiceException ex) => _notificationService.NotifyAsync(ex);
}
