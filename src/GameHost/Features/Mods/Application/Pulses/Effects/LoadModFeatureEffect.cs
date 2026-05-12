using GameHost.Features.Mods.Application.Mediator.Queries;
using GameHost.Features.Mods.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal class LoadModFeatureEffect : IEffect<LoadModFeatureAction>
{
    private readonly IMedihater _medihater;

    public LoadModFeatureEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(LoadModFeatureAction action, IDispatcher dispatcher)
    {
        var data = new GetModFeatureQuery();
        var result = await _medihater.Send(data);
        await dispatcher.Prepare<LoadModFeatureDoneAction>().With(p => p.ModFeature, result).DispatchAsync();
    }
}
