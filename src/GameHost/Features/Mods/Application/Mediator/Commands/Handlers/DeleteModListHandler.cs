using GameHost.Features.Mods.Application.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class DeleteModListHandler : HandlerBase, IRequestHandler<DeleteModListCommand>
{
    private readonly IModListService _modListService;

    public DeleteModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<DeleteModListHandler> crazyReport) : base(notificationService, crazyReport)
    {
        _modListService = modListService;
    }

    public async Task Handle(DeleteModListCommand request, CancellationToken cancellationToken)
    => await ExecAndHandleExceptions(
                () => _modListService.DeleteAsync(request.Id, cancellationToken),
                ex => throw ex
                );
}
