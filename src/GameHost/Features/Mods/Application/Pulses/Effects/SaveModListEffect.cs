using GameHost.Features.Mods.Application.Mediator.Commands;
using GameHost.Features.Mods.Application.Pulses.Actions;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal sealed class SaveModListEffect : IEffect<SaveModListAction>
{
    private readonly IMedihater _medihater;

    public SaveModListEffect(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task EffectAsync(SaveModListAction action, IDispatcher dispatcher)
    {
        var command = new SaveModListCommand(action.ModListEntity);
        await _medihater.Send(command);

        await dispatcher
            .Prepare<SaveModListDoneAction>()
            .DispatchAsync();
    }
}
