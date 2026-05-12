using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class CreateModListDoneReducer : IReducer<ModListLocalState, CreateModListDoneAction>
{
    public ModListLocalState Reduce(ModListLocalState state, CreateModListDoneAction action)
        => state with
        {
            DidLastCreationFailed = action.Failed,
            IsCurrentLoading = false
        };
}
