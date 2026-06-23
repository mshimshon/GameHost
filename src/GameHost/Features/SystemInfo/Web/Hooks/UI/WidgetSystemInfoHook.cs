using GameHost.Features.SystemInfo.Web.Hooks.UI.Components;
using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;

namespace GameHost.Features.SystemInfo.Web.Hooks.UI;

[EngineBusKey(DashboardKeys.UI.GetWidgets)]
public class WidgetSystemInfoHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetSystemInfo>();
}
