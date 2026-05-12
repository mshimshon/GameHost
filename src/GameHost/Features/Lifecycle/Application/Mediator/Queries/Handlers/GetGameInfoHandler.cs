using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Features.Lifecycle.Domain.Entites;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries.Handlers;

internal sealed class GetGameInfoHandler : HandlerBase, IRequestHandler<GetGameInfoQuery, GameInfoEntity?>
{
    private readonly IGameInfoService _gameInfoService;

    public GetGameInfoHandler(IGameInfoService gameInfoService, INotificationService notificationService, ICrazyReport<GetGameInfoHandler> logger) : base(notificationService, logger)
    {
        _gameInfoService = gameInfoService;
        logger.SetModule(LifecycleKeys.MODULE_NAME);
    }
    public async Task<GameInfoEntity?> Handle(GetGameInfoQuery request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                () => _gameInfoService.LoadGameInfoAsync(cancellationToken),
                () => default
                );
}
