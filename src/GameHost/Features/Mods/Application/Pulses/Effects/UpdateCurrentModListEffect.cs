using GameHost.Features.Mods.Application.Mediator.Commands;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal class UpdateCurrentModListEffect : IEffect<UpdateCurrentModListAction>
{
    private readonly IMedihater _medihater;
    private readonly INotificationService _notification;

    public UpdateCurrentModListEffect(IMedihater medihater, INotificationService notification)
    {
        _medihater = medihater;
        _notification = notification;
    }
    public async Task EffectAsync(UpdateCurrentModListAction action, IDispatcher dispatcher)
    {
        // TODO: MEDIHATER
        var command = new UpdateCurrentModlistCommand(action.Current?.Id);
        try
        {
            await _medihater.Send(command);
            await dispatcher.Prepare<UpdateCurrentModListDoneAction>()
                .With(p => p.Current, action.Current)
                .DispatchAsync();
        }
        catch (Exception)
        { }
    }
}
