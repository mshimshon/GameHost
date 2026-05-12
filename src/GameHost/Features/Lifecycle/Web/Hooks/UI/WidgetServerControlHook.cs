using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Web.Hooks.UI.Components;
using GameHost.Kernel.Abstractions.Services.HostStateHookService;
using GameHost.Kernel.Services.HostStateHookService;
using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Abstraction.Tools;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;

namespace GameHost.Features.Lifecycle.Web.Hooks.UI;

[EngineBusId(DashboardKeys.UI.GetWidgets)]
internal class WidgetServerControlHook : HostStateHookBase, IEngineBusHandler
{

    public WidgetServerControlHook(IPanelControl panelControl, IHostStateHookRegistry hostStateHookRegistry, IServiceProvider serviceProvider) :
        base(panelControl, hostStateHookRegistry, serviceProvider)
    {
        HostReactToState<GameInfoState>(nameof(IPanelControl.LayoutRender));
    }
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetServerControl>(p => p with
        {
            VisibilityCondition = () =>
            {
                var state = GetState<GameInfoState>()!;
                return state.GameInfo != default && state.GameInfo.StartupParameters != default && state.SavedParametersLoaded;
            }

        });


}
