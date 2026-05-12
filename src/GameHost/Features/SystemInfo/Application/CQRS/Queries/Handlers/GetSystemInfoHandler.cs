using GameHost.Core.Features;
using GameHost.Features.SystemInfo.Application.Services;
using GameHost.Features.SystemInfo.Domain.Entites;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.SystemInfo.Application.CQRS.Queries.Handlers;

internal class GetSystemInfoHandler : HandlerBase, IRequestHandler<GetSystemInfoQuery, SystemInfoEntity?>
{
    private readonly ISystemInfoService _systemInfoService;

    public GetSystemInfoHandler(ISystemInfoService systemInfoService, INotificationService notificationService, ICrazyReport<GetSystemInfoHandler> logger) : base(notificationService, logger)
    {
        _systemInfoService = systemInfoService;
        logger.SetModule(SystemInfoKeys.MODULE_NAME);
    }

    public async Task<SystemInfoEntity?> Handle(GetSystemInfoQuery request, CancellationToken cancellationToken)
    {
        return await ExecAndHandleExceptions(
        () => _systemInfoService.GetSystemInfoAsync(cancellationToken),
        () => default
        );

    }
}
