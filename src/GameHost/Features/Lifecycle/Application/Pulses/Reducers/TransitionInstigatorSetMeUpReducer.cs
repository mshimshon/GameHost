using LunaticPanel.Core.Abstraction.Plugin;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Reducers;

internal class TransitionInstigatorSetMeUpReducer : IReducer<ServerTransitionState, TransitionInstigatorSetMeUpAction>
{
    private readonly IPluginContext _pluginContext;

    public TransitionInstigatorSetMeUpReducer(IPluginContext pluginContext)
    {
        _pluginContext = pluginContext;
    }
    public ServerTransitionState Reduce(ServerTransitionState state, TransitionInstigatorSetMeUpAction action)
    {
        return state with { AmInstigator = true };
    }
}
