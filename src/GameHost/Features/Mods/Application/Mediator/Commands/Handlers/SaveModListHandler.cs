using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Payloads.Responses.Mapping;
using GameHost.Features.Mods.Application.Services;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class SaveModListHandler : IRequestHandler<SaveModListCommand>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<SaveModListHandler> _crazyReport;

    public SaveModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<SaveModListHandler> crazyReport)
    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task Handle(SaveModListCommand request, CancellationToken ct)
       => await request
        .HandleWithData(request.ModList.MapToDomain(), _modListService.SaveAsync, OnFailure)
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
