using GameHost.Features.Lifecycle.Application.Mediator.Commands;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal class ServerStopEffect : IEffect<ServerStopAction>
{
    private readonly IMedihater _medihater;

    public ServerStopEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(ServerStopAction action, IDispatcher dispatcher)
    {
        await dispatcher.Prepare<TransitionInstigatorSetMeUpAction>().DispatchAsync();
        var exec = new ExecStopServerCommand();
        await _medihater.Send(exec);

    }
}
