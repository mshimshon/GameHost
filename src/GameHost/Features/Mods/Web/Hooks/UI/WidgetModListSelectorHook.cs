using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Web.Hooks.UI.Components;
using GameHost.Kernel.Abstractions.Services.HostStateHookService;
using GameHost.Kernel.Services.HostStateHookService;
using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Abstraction.Tools;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;

namespace GameHost.Features.Mods.Web.Hooks.UI;

[EngineBusKey(DashboardKeys.UI.GetWidgets)]
internal class WidgetModListSelectorHook : HostStateHookBase, IEngineBusHandler
{

    private bool _lastLoadingState;

    public WidgetModListSelectorHook(IPanelControl panelControl, IHostStateHookRegistry hostStateHookRegistry, IServiceProvider serviceProvider) :
        base(panelControl, hostStateHookRegistry, serviceProvider)
    {
        HostReactToState<ModListState>(nameof(IPanelControl.DashboardRender));

    }


    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetModlistSelector>(p => p with
        {
            VisibilityCondition = () =>
            {
                var state = GetState<ModListState>();
                return state!.FeatureInfo != default && state!.FeatureInfo!.IsEnabled;
            }
        });
}



