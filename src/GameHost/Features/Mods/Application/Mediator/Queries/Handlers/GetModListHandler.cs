using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Domain.Entities;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal class GetModListHandler : HandlerBase, IRequestHandler<GetModListQuery, ModListEntity?>
{
    private readonly IModListService _modListService;

    public GetModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.MODULE_NAME);
    }
    public async Task<ModListEntity?> Handle(GetModListQuery request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                () => _modListService.GetAsync(request.Id, cancellationToken),
                () => default
                );

}
