using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Domain.ValueObjects;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal class GetAllModListHandler : HandlerBase, IRequestHandler<GetAllModListQuery, ICollection<ModListDescriptor>>
{
    private readonly IModListService _modListService;

    public GetAllModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetAllModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task<ICollection<ModListDescriptor>> Handle(GetAllModListQuery request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                () => _modListService.GetAllAsync(cancellationToken),
                () => new List<ModListDescriptor>()
                );
}
