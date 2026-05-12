using GameHost.Features.Lifecycle.Application.Mediator.Queries;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal class ServerStatusUpdateEffect : IEffect<ServerStatusUpdateAction>
{
    private readonly IMedihater _medihater;
    private readonly IStateAccessor<ServerState> _serverStatusStateAccess;

    public ServerStatusUpdateEffect(IMedihater medihater, IStateAccessor<ServerState> serverStatusStateAccess)
    {
        _medihater = medihater;
        _serverStatusStateAccess = serverStatusStateAccess;
    }
    public async Task EffectAsync(ServerStatusUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetServerStatusQuery();
        var serverInfo = await _medihater.Send(exec);
        await dispatcher.Prepare<TransitionAction>().With(p => p.ServerInfo, serverInfo).DispatchAsync();
        await dispatcher.Prepare<ServerStatusUpdateDoneAction>().With(p => p.ServerInfo, serverInfo).DispatchAsync();

    }
}
