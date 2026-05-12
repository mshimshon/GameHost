using LunaticPanel.Core.Utils.Abstraction.Logging;
using GameHost.Features.Mods.Application.Services;
using GameHost.Kernel.Abstractions.Mediator;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal class GetCurrentModListHandler : HandlerBase, IRequestHandler<GetCurrentModListQuery, Guid?>
{
    private readonly IModListService _modListService;

    public GetCurrentModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;

    }
    public async Task<Guid?> Handle(GetCurrentModListQuery request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                () => _modListService.GetCurrentAsync(cancellationToken),
                () => default
                );
}
