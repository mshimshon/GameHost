using CoreMap;
using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries.Handlers;

internal class GetAvailableGameManifestsHandler : IRequestHandler<GetAvailableGameManifestsQuery, ICollection<GameManifestResponse>?>
{
    private readonly ILinuxGameServerService _linuxGameServerService;
    private readonly ICoreMap _coreMap;
    private readonly INotificationService _notificationService;
    private readonly ICrazyReport<GetAvailableGameManifestsHandler> _crazyReport;

    public GetAvailableGameManifestsHandler(ILinuxGameServerService linuxGameServerService,
        ICoreMap coreMap,
        INotificationService notificationService,
        ICrazyReport<GetAvailableGameManifestsHandler> crazyReport)
    {
        _linuxGameServerService = linuxGameServerService;
        _coreMap = coreMap;
        _notificationService = notificationService;
        _crazyReport = crazyReport;
    }

    public Task<ICollection<GameManifestResponse>?> Handle(GetAvailableGameManifestsQuery request, CancellationToken cancellationToken)
        => request.Handle(HandleRequest, (ex) => _notificationService.HandleUnknownException(_crazyReport, ex))
        .HandleExceptionFor<WebServiceException>(_notificationService.NotifyAsync)
        .ExecOrDefaultAsync<ICollection<GameManifestResponse>>(cancellationToken);

    private async Task<ICollection<GameManifestResponse>?> HandleRequest(CancellationToken cancellationToken)
    {
        var result = await _linuxGameServerService.GetAvailableGames(cancellationToken);
        if (result == default) return default;
        return _coreMap.MapEach(result).To<GameManifestResponse>();
    }
}
