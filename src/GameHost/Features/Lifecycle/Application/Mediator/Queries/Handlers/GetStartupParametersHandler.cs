using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using GameHost.Kernel.Abstractions.Mediator;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries.Handlers;

public class GetStartupParametersHandler : HandlerBase, IRequestHandler<GetStartupParametersQuery, Dictionary<string, string>>
{
    private readonly IStartupParameterService _startupParameterService;

    public GetStartupParametersHandler(IStartupParameterService startupParameterService, INotificationService notificationService, ICrazyReport<GetStartupParametersHandler> logger) : base(notificationService, logger)
    {
        _startupParameterService = startupParameterService;
        logger.SetModule(LifecycleKeys.MODULE_NAME);
    }
    public async Task<Dictionary<string, string>> Handle(GetStartupParametersQuery request, CancellationToken cancellationToken)
    {
        return await ExecAndHandleExceptions(
            () => _startupParameterService.GetServerStartupParametersAsync(cancellationToken),
            () => new()
            );
    }
}
