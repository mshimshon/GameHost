using CoreMap;
using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Contracts.Responses;
using GameHost.Features.Mods.Application.Services;
using GameHost.Kernel.Abstractions.Mediator;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries.Handlers;

internal class GetModFeatureHandler : HandlerBase, IRequestHandler<GetModFeatureQuery, ModFeatureResponse?>
{
    private readonly ICoreMap _coreMap;
    private readonly IModListService _modListService;

    public GetModFeatureHandler(ICoreMap coreMap, IModListService modListService, INotificationService notificationService, ICrazyReport<GetModFeatureHandler> crazyReport) : base(notificationService, crazyReport)
    {
        _coreMap = coreMap;
        _modListService = modListService;
        crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }
    private async Task<ModFeatureResponse?> Logic(CancellationToken ct = default)
    {
        var result = await _modListService.GetModFeatureAsync(ct);
        if (result == default) return default;
        var mappedResult = _coreMap.Map(result).To<ModFeatureResponse>();
        return mappedResult;
    }
    public async Task<ModFeatureResponse?> Handle(GetModFeatureQuery request, CancellationToken ct)
          => await ExecAndHandleExceptions(() => Logic(ct), () => default);
}
