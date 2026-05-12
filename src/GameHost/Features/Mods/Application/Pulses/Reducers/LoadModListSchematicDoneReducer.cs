using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class LoadModListSchematicDoneReducer : IReducer<ModListState, LoadModListSchematicDoneAction>
{
    public ModListState Reduce(ModListState state, LoadModListSchematicDoneAction action)
        => state with
        {
            IsSchematicPartsLoading = false,
            IsSchematicPartsLoaded = state.IsSchematicPartsLoaded ? true : action.SchematicParts != default,
            SchematicParts = action.SchematicParts ?? Array.Empty<PartSchematicEntity>().AsReadOnly()
        };
}
