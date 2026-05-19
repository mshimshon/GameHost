using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Domain.ValueObjects;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands.Handlers;

internal class CreateModListHandler : IRequestHandler<CreateModListCommand>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<CreateModListHandler> _crazyReport;

    public CreateModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<CreateModListHandler> crazyReport)
    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task Handle(CreateModListCommand request, CancellationToken ct)
       => await request
        .HandleWithData(new ModListDescriptor(request.Id, request.Name), _modListService.CreateAsync, OnFailure)
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
