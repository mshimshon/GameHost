using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Models;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Kernel.Abstractions.Mediator;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries.Handlers;

internal class GetInstallationProgressHandler : HandlerBase, IRequestHandler<GetInstallationProgressQuery, GameServerInstallProcessModel?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;

    public GetInstallationProgressHandler(ILinuxGameServerService linuxGameServerService, INotificationService notificationService, ICrazyReport<GetInstallationProgressHandler> logger)
        : base(notificationService, logger)
    {
        _linuxGameServerService = linuxGameServerService;
        logger.SetModule(LinuxGameServerKeys.MODULE_NAME);
    }

    public async Task<GameServerInstallProcessModel?> Handle(GetInstallationProgressQuery request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(
        () => _linuxGameServerService.GetInstallationProgress(cancellationToken),
        () =>
        {
            return default;
        }
        );
}
