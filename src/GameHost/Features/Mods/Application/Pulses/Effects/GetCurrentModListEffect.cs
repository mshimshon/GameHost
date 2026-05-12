using GameHost.Features.Mods.Application.Mediator.Queries;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal sealed class GetCurrentModListEffect : IEffect<GetCurrentModListAction>
{
    private readonly IMedihater _medihater;
    private readonly INotificationService _notificationService;
    private readonly IStateAccessor<ModListState> _modListStateAccess;

    public GetCurrentModListEffect(IMedihater medihater, INotificationService notificationService, IStateAccessor<ModListState> modListStateAccess)
    {
        _medihater = medihater;
        _notificationService = notificationService;
        _modListStateAccess = modListStateAccess;
    }
    public async Task EffectAsync(GetCurrentModListAction action, IDispatcher dispatcher)
    {
        var query = new GetCurrentModListQuery();
        var result = await _medihater.Send(query);
        if (result == default)
        {
            await dispatcher.Prepare<GetCurrentModListDoneAction>()
                .With(p => p.Current, default)
                .DispatchAsync();
            return;
        }

        bool cannotMatchAvailable = result != default && result != Guid.Empty && _modListStateAccess.State.Available.Count <= 0;
        if (cannotMatchAvailable)
        {
            await _notificationService.NotifyAsync("No Available Modlist Exist to Get the current.", NotificationSeverity.Warning); // TODO: Localize
            return;
        }
        var foundMatchElement = _modListStateAccess.State.Available.SingleOrDefault(p => p.Id == result);
        var noMatchWasFound = foundMatchElement == default;
        if (noMatchWasFound)
        {
            await _notificationService.NotifyAsync($"No match was found for currently set modlist {result}", NotificationSeverity.Warning); // TODO: Localize
            await dispatcher.Prepare<UpdateCurrentModListAction>().With(p => p.Current, default)
                .DispatchAsync();
            return;
        }

        await dispatcher.Prepare<GetCurrentModListDoneAction>()
            .With(p => p.Current, foundMatchElement)
            .DispatchAsync();

    }
}
