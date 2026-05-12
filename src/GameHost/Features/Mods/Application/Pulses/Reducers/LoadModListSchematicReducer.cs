using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class LoadModListSchematicReducer : IReducer<ModListState, LoadModListSchematicAction>
{
    public ModListState Reduce(ModListState state, LoadModListSchematicAction action)
        => state with
        {
            IsSchematicPartsLoading = true
        };
}
