using GameHost.Core.Features;
using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using GameHost.Features.SystemInfo.Application.Payloads.Responses.Mapping;
using GameHost.Features.SystemInfo.Application.Services;
using GameHost.Features.SystemInfo.Domain.Entites;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.SystemInfo.Application.Mediator.Queries.Handlers;

internal class GetSystemInfoHandler : IRequestHandler<GetSystemInfoQuery, SystemInfoResponse?>
{
    private readonly ISystemInfoService _systemInfoService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetSystemInfoHandler> _crazyReport;

    public GetSystemInfoHandler(ISystemInfoService systemInfoService, INotificationService notificationService, ICrazyReport<GetSystemInfoHandler> crazyReport)
    {
        _systemInfoService = systemInfoService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(SystemInfoKeys.MODULE_NAME);
    }

    public async Task<SystemInfoResponse?> Handle(GetSystemInfoQuery request, CancellationToken ct)
       => await request
        .Handle(_systemInfoService.GetSystemInfoAsync, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecOrDefaultAsync<SystemInfoEntity, SystemInfoResponse>(e => e.MapToApplication(), ct);
    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
    }
}
