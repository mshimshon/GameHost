using LunaticPanel.Core.Utils.Abstraction.Logging;
using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Kernel.Abstractions.Mediator;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries.Handlers;

internal class GetRawGameInfoHandler : HandlerBase, IRequestHandler<GetRawGameInfoQuery, string?>
{
    private readonly IGameInfoService _gameInfoReader;

    public GetRawGameInfoHandler(IGameInfoService gameInfoReader, INotificationService notificationService, ICrazyReport<GetRawGameInfoHandler> logger) : base(notificationService, logger)
    {
        _gameInfoReader = gameInfoReader;
        logger.SetModule(LifecycleKeys.MODULE_NAME);
    }

    public async Task<string?> Handle(GetRawGameInfoQuery request, CancellationToken cancellationToken)
    {
        return
            await ExecAndHandleExceptions(
                () => _gameInfoReader.GetRawGameInfoAsync(cancellationToken),
                () => default
                );

    }
}
