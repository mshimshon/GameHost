using GameHost.Features.Mods.Domain.Entities;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

public sealed record GetModSchematicQuery : IRequest<IReadOnlyCollection<PartSchematicEntity>?>
{
}
