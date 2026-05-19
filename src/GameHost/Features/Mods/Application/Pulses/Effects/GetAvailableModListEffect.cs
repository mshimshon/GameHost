using GameHost.Features.Mods.Application.Mediator.Queries;
using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal sealed class GetAvailableModListEffect : IEffect<GetAvailableModListAction>
{
    private readonly IMedihater _medihater;

    public GetAvailableModListEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(GetAvailableModListAction action, IDispatcher dispatcher)
    {
        var command = new GetAllModListQuery();
        var result = await _medihater.Send(command);
        await dispatcher.Prepare<GetAvailableModListDoneAction>()
            .With(p => p.Available, result?.ToList() ?? new List<ModListDescriptorResponse>())
            .DispatchAsync();
    }
}
