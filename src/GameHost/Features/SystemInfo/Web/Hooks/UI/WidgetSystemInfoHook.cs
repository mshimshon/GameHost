using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;
using GameHost.Features.SystemInfo.Web.Hooks.UI.Components;

namespace GameHost.Features.SystemInfo.Web.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
public class WidgetSystemInfoHook : IEngineBusHandler
{
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetSystemInfo>();
}
