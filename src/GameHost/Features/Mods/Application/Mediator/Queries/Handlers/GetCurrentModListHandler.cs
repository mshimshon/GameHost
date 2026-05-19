using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal class GetCurrentModListHandler : IRequestHandler<GetCurrentModListQuery, Guid?>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetCurrentModListHandler> _crazyReport;

    public GetCurrentModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetCurrentModListHandler> crazyReport)

    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }
    public async Task<Guid?> Handle(GetCurrentModListQuery request, CancellationToken ct)
            => await request
        .Handle(_modListService.GetCurrentAsync, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecOrDefaultAsync<Guid>(ct);

    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
    }
}
