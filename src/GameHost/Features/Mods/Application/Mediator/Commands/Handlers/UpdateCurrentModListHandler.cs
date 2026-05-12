using GameHost.Features.Mods.Application.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class UpdateCurrentModListHandler : HandlerBase, IRequestHandler<UpdateCurrentModlistCommand>
{
    private readonly IModListService _modListService;

    public UpdateCurrentModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport crazyReport) : base(notificationService, crazyReport)
    {
        _modListService = modListService;
    }

    public async Task Handle(UpdateCurrentModlistCommand request, CancellationToken cancellationToken)
                => await ExecAndHandleExceptions(
                    () => _modListService.SetCurrentAsync(request.Id, cancellationToken),
                    ex => throw ex
                    );
}
