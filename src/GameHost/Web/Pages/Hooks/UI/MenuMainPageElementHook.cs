using GameHost.Web.Pages.Hooks.UI.Components;
using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;

namespace GameHost.Web.Pages.Hooks.UI;

[EngineBusKey(MainMenuKeys.UI.GetElements)]
public class MenuMainPageElementHook : IEngineBusHandler
{

    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetMainPageMenuLink>();
}
