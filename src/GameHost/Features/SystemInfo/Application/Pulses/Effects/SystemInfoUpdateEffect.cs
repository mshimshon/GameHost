using GameHost.Features.SystemInfo.Application.CQRS.Queries;
using GameHost.Features.SystemInfo.Application.Pulses.Actions;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.Effects;

internal class SystemInfoUpdateEffect : IEffect<SystemInfoUpdateAction>
{
    private readonly IStateAccessor<SystemInfoState> _stateAccessor;
    private readonly IMedihater _medihater;

    public SystemInfoUpdateEffect(
        IStateAccessor<SystemInfoState> stateAccessor,
        IMedihater medihater)
    {
        _stateAccessor = stateAccessor;
        _medihater = medihater;
    }

    public async Task EffectAsync(SystemInfoUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetSystemInfoQuery();
        var serverInfo = await _medihater.Send(exec);

        if (serverInfo != default && serverInfo.Disk != default && serverInfo.Processor != default && serverInfo.Memory != default)
            await dispatcher.Prepare<SystemInfoUpdatedAction>()
                .With(p => p.SystemInfo, serverInfo)
                .DispatchAsync();
    }
}
