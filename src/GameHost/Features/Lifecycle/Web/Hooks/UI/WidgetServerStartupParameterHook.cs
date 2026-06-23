using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Web.Hooks.UI.Components;
using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Web.Hooks.UI;

[EngineBusKey(DashboardKeys.UI.GetWidgets)]
internal class WidgetServerStartupParameterHook : IEngineBusHandler
{
    private readonly IStateAccessor<GameInfoState> _stateGameInfoAccess;

    public WidgetServerStartupParameterHook(IStateAccessor<GameInfoState> stateGameInfoAccess)
    {
        _stateGameInfoAccess = stateGameInfoAccess;
    }
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetStartupParameters>(p => p with
        {
            VisibilityCondition = () => _stateGameInfoAccess.State.GameInfo != default
        });
}
