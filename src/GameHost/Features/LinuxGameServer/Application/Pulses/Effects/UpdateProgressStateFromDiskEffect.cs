using GameHost.Features.LinuxGameServer.Application.Mediator.Queries;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Effects;

internal class UpdateProgressStateFromDiskEffect : IEffect<UpdateProgressStateFromDiskAction>
{
    private readonly IMedihater _medihater;

    public UpdateProgressStateFromDiskEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(UpdateProgressStateFromDiskAction action, IDispatcher dispatcher)
    {
        var result = await _medihater.Send(new GetInstallationProgressQuery());
        await dispatcher.Prepare<UpdateProgressStateFromDiskDoneAction>()
            .With(p => p.ProgressState, result)
            .DispatchAsync();
    }
}
