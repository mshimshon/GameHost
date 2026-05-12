using GameHost.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record LoadModListSchematicDoneAction : IAction
{
    public IReadOnlyCollection<PartSchematicEntity>? SchematicParts { get; set; }
}
