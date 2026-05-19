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

internal sealed class GetModSchematicHandler : IRequestHandler<GetModSchematicQuery, ICollection<PartSchematicResponse>?>
{
    private readonly IModListService _modListService;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetModSchematicHandler> _crazyReport;

    public GetModSchematicHandler(IModListService modListService, INotificationService notificationService,
        ICrazyReport<GetModSchematicHandler> crazyReport)
    {
        _modListService = modListService;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }

    public async Task<ICollection<PartSchematicResponse>?> Handle(GetModSchematicQuery request, CancellationToken ct)
       => await request
        .Handle(_modListService.GetSchematic, OnFailure)
        .HandleExceptionFor<WebServiceException>(OnFailure)
        .ExecOrDefaultAsync<IReadOnlyCollection<ModSchemaPartEntity>, ICollection<PartSchematicResponse>>(
           e => e.Select(p => p.MapToApplication()).ToList(), ct);
    private async Task OnFailure(Exception ex)
    {
        await _notificationService.HandleUnknownException(_crazyReport, ex);
    }

    private async Task OnFailure(WebServiceException ex)
    {
        await _notificationService.NotifyAsync(ex);
    }
}
