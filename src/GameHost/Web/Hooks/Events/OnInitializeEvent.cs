using GameHost.Keys;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;

namespace GameHost.Web.Hooks.Events;

[EventBusKey(GameHostKeys.Events.ON_BEFORE_RUNTIME_INITIALIZATION)]
internal class OnInitializeEvent : IEventBusHandler
{
    private readonly ILinuxCommand _linuxCommand;

    public OnInitializeEvent(ILinuxCommand linuxCommand)
    {
        _linuxCommand = linuxCommand;
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        await _linuxCommand
            .BuildCommand("id -u lgsm >/dev/null 2>&1")
            .OrCommand("useradd -m -d /home/lgsm -s /usr/sbin/nologin lgsm")
            .ExecAsync();

    }
}
