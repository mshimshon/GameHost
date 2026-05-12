using GameHost.Features.Lifecycle.Application.Mediator.Commands;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal class ServerStartEffect : IEffect<ServerStartAction>
{
    private readonly IMedihater _medihater;

    public ServerStartEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(ServerStartAction action, IDispatcher dispatcher)
    {
        await dispatcher.Prepare<TransitionInstigatorSetMeUpAction>().DispatchAsync();
        var exec = new ExecStartServerCommand();
        await _medihater.Send(exec);
    }
}
