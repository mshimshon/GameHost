using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Hooks.Events;

[EventBusId(ModListKeys.Events.ON_FILES_IN_MONITORED_MOD_LIST_FOLDER_CHANGED, CrossCircuitReceiver = EventBusSpreadType.CrossCircuitExcludeSender)]
internal class CloseOpenedModListWhenDeleted : IEventBusHandler
{
    private readonly IStateAccessor<ModListLocalState> _modListLocalStateAccess;
    private readonly IStateAccessor<ModListState> _modListStateAccess;
    private readonly IDispatcher _dispatcher;

    public CloseOpenedModListWhenDeleted(IStateAccessor<ModListLocalState> modListLocalStateAccess, IStateAccessor<ModListState> modListStateAccess,
        IDispatcher dispatcher)
    {
        _modListLocalStateAccess = modListLocalStateAccess;
        _modListStateAccess = modListStateAccess;
        _dispatcher = dispatcher;
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        bool hasModListOpenInEditor = _modListLocalStateAccess.State.Current != default;
        if (!hasModListOpenInEditor) return;
        var wasCurrentDelete = !_modListStateAccess.State.Available.Any(p => p.Id == _modListLocalStateAccess.State.Current!.Descriptor.Id);
        if (wasCurrentDelete)
            await _dispatcher.Prepare<CloseModListAction>().DispatchAsync();


    }
}
