using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class CreateModListHandler : HandlerBase, IRequestHandler<CreateModListCommand>
{
    private readonly IModListService _modListService;

    public CreateModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<CreateModListHandler> logger) :
        base(notificationService, logger)
    {
        _modListService = modListService;
        logger.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task Handle(CreateModListCommand request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                () => _modListService.CreateAsync(new(request.Id, request.Name), cancellationToken),
                ex => throw ex
                );
}
