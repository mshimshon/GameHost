using GameHost.Features.Lifecycle.Application.Mediator.Queries;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal class FetchStartupParametersEffect : IEffect<FetchStartupParametersAction>
{
    private readonly IMedihater _medihater;

    public FetchStartupParametersEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(FetchStartupParametersAction action, IDispatcher dispatcher)
    {

        var exec = new GetStartupParametersQuery();
        Dictionary<string, string>? data = default;
        data = await _medihater.Send(exec);
        await dispatcher
            .Prepare<FetchStartupParametersDoneAction>()
            .With(p => p.StartupParameters, data)
            .DispatchAsync();
    }
}
