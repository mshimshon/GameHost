using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal class CreateModListReducer : IReducer<ModListLocalState, CreateModListAction>
{
    public ModListLocalState Reduce(ModListLocalState state, CreateModListAction action) => state with
    {
        DidLastCreationFailed = false,
        IsCurrentLoading = true
    };
}
