using GameHost.Features.Mods.Application.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class SaveModListHandler : HandlerBase, IRequestHandler<SaveModListCommand>
{
    private readonly IModListService _modListService;

    public SaveModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<SaveModListHandler> crazyReport) : base(notificationService, crazyReport)
    {
        _modListService = modListService;
    }

    public async Task Handle(SaveModListCommand request, CancellationToken cancellationToken)
        => await ExecAndHandleExceptions(
                    () => _modListService.SaveAsync(request.ModListEntity, cancellationToken),
                    ex => throw ex
                    );
}
