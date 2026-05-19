using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Application.Payloads.Responses.Mapping;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal class GetModListHandler : IRequestHandler<GetModListQuery, ModListResponse?>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetModListHandler> _crazyReport;

    public GetModListHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModListHandler> crazyReport)
    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }
    public async Task<ModListResponse?> Handle(GetModListQuery request, CancellationToken ct)
        => await request
        .HandleWithData(request.Id, _modListService.GetAsync, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecOrDefaultAsync<ModListEntity, ModListResponse>(e => e.MapToApplication(), ct);
    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
    }
}
