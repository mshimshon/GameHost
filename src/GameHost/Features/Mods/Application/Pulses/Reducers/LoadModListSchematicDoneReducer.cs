using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Reducers;

internal sealed class LoadModListSchematicDoneReducer : IReducer<ModListState, LoadModListSchematicDoneAction>
{
    public ModListState Reduce(ModListState state, LoadModListSchematicDoneAction action)
        => state with
        {
            IsSchematicPartsLoading = false,
            IsSchematicPartsLoaded = state.IsSchematicPartsLoaded ? true : action.SchematicParts != default,
            SchematicParts = action.SchematicParts?.ToList().AsReadOnly() ?? Array.Empty<PartSchematicResponse>().AsReadOnly()
        };
}
