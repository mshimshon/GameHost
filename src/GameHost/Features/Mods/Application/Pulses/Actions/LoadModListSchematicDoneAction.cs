using GameHost.Features.Mods.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record LoadModListSchematicDoneAction : IAction
{
    public ICollection<PartSchematicResponse>? SchematicParts { get; set; }
}
