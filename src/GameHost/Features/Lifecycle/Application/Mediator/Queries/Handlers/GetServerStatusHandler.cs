using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Features.Lifecycle.Domain.Entites;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries.Handlers;

public class GetServerStatusHandler : HandlerBase, IRequestHandler<GetServerStatusQuery, ServerInfoEntity?>
{
    private readonly ILifecycleServices _lifecycleServices;

    public GetServerStatusHandler(ILifecycleServices lifecycleServices, INotificationService notificationService, ICrazyReport<GetServerStatusHandler> logger) : base(notificationService, logger)
    {
        _lifecycleServices = lifecycleServices;
        logger.SetModule(LifecycleKeys.MODULE_NAME);
    }
    public async Task<ServerInfoEntity?> Handle(GetServerStatusQuery request, CancellationToken cancellationToken)
    {
        return
            await ExecAndHandleExceptions(
                () => _lifecycleServices.ServerStatusAsync(cancellationToken),
                () => default
                );

    }
}
