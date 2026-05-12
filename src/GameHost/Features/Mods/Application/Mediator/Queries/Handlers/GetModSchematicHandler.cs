using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Domain.Entities;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal sealed class GetModSchematicHandler : HandlerBase, IRequestHandler<GetModSchematicQuery, IReadOnlyCollection<PartSchematicEntity>?>
{
    private readonly IModListService _modListService;

    public GetModSchematicHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModSchematicHandler> logger) : base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task<IReadOnlyCollection<PartSchematicEntity>?> Handle(GetModSchematicQuery request, CancellationToken cancellationToken)
            => await ExecAndHandleExceptions(
                () => _modListService.GetSchematic(cancellationToken),
                () => default
                );
}
