using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Kernel.Abstractions.Mediator;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
namespace GameHost.Features.Lifecycle.Application.Mediator.Commands.Handlers;

public class ExecUpdateStartupParameterHandler : HandlerBase, IRequestHandler<ExecUpdateStartupParameterCommand>
{
    private readonly IStartupParameterService _startupParameterService;

    public ExecUpdateStartupParameterHandler(IStartupParameterService startupParameterService, INotificationService notificationService, ICrazyReport<ExecUpdateStartupParameterHandler> logger) : base(notificationService, logger)
    {
        _startupParameterService = startupParameterService;
        logger.SetModule(LifecycleKeys.MODULE_NAME);
    }
    public async Task Handle(ExecUpdateStartupParameterCommand request, CancellationToken cancellationToken)
    {
        await ExecAndHandleExceptions(() => _startupParameterService.UpdateStartupParameterAsync(request.Key, request.Value, cancellationToken));
        //TODO: Implement the following inside the service level
        //if (_gameinfoStateAccessor.State.StartupParameters.ContainsKey(request.Key))
        //    _gameinfoStateAccessor.State.StartupParameters[request.Key] = request.Value;
        //else
        //    _gameinfoStateAccessor.State.StartupParameters.Add(request.Key, request.Value);

        //await _dispatcher.Prepare<LifecycleServerGameInfoUpdatedAction>()
        //    .With(p => p.GameInfo, _gameinfoStateAccessor.State.GameInfo)
        //    .DispatchAsync();
    }
}
