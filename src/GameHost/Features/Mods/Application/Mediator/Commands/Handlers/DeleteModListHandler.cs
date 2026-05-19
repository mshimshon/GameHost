using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class DeleteModListHandler : IRequestHandler<DeleteModListCommand>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<DeleteModListHandler> _crazyReport;

    public DeleteModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<DeleteModListHandler> crazyReport)
    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task Handle(DeleteModListCommand request, CancellationToken ct)
       => await request
        .HandleWithData(request.Id, _modListService.DeleteAsync, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecAsync(ct);
    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
    }
}
