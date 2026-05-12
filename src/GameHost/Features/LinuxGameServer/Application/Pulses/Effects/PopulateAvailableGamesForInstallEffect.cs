using GameHost.Features.LinuxGameServer.Application.Mediator.Queries;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Effects;

internal class PopulateAvailableGamesForInstallEffect : IEffect<PopulateAvailableGamesForInstallAction>
{
    private readonly IMedihater _medihater;

    public PopulateAvailableGamesForInstallEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(PopulateAvailableGamesForInstallAction action, IDispatcher dispatcher)
    {
        var result = await _medihater.Send(new GetAvailableGameManifestsQuery(), dispatcher.CancelToken);
        await dispatcher.Prepare<PopulateAvailableGamesForInstallDoneAction>()
            .With(p => p.GameManifests, result)
            .DispatchAsync();
    }
}
