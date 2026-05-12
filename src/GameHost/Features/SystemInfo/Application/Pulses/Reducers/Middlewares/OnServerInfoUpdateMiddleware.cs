using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using GameHost.Core.Features;
using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.Reducers.Middlewares;

internal class OnServerInfoUpdateMiddleware : IReducerMiddleware
{
    private readonly IEventBus _eventBus;

    public OnServerInfoUpdateMiddleware(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public async Task AfterReducing(object state, object action)
    {
        if (typeof(SystemInfoState) != state.GetType()) return;
        if (typeof(SystemInfoUpdatedAction) != action.GetType()) return;
        await _eventBus.PublishDataAsync(SystemInfoKeys.Events.OnStateUpdate, state);

    }
    public Task BeforeReducing(object state, object action) => Task.CompletedTask;
}
