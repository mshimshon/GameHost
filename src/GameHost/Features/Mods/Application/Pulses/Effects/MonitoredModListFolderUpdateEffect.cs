using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using GameHost.Features.Mods.Application.Pulses.Actions;
using StatePulse.Net;
using GameHost.Keys.Features;

namespace GameHost.Features.Mods.Application.Pulses.Effects;

internal class MonitoredModListFolderUpdateEffect : IEffect<MonitoredModListFolderUpdateAction>
{
    private readonly IEventBus _eventBus;

    public MonitoredModListFolderUpdateEffect(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task EffectAsync(MonitoredModListFolderUpdateAction action, IDispatcher dispatcher)
    {
        Console.WriteLine($"MonitoredModListFolderUpdateEffect -> Reacted to Changes");

        await dispatcher.Prepare<GetAvailableModListAction>().Await().DispatchAsync();
        await dispatcher.Prepare<GetCurrentModListAction>().Await().DispatchAsync();

        await _eventBus.PublishDatalessAsync(ModListKeys.Events.ON_FILES_IN_MONITORED_MOD_LIST_FOLDER_CHANGED);
    }
}
