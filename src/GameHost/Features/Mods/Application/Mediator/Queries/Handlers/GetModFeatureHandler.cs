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

internal class GetModFeatureHandler : IRequestHandler<GetModFeatureQuery, ModFeatureResponse?>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetModFeatureHandler> _crazyReport;

    public GetModFeatureHandler(IModListService modListService, INotificationService notificationService, ICrazyReport<GetModFeatureHandler> crazyReport)
    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task<ModFeatureResponse?> Handle(GetModFeatureQuery request, CancellationToken ct)
        => await request
        .Handle(_modListService.GetModFeatureAsync, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecOrDefaultAsync<ModFeatureEntity, ModFeatureResponse>(e => e.MapToApplication(), ct);

    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
    }
}
