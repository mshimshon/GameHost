using GameHost.Features.Mods.Application.Payloads.Responses;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

public sealed record GetModSchematicQuery : IRequest<ICollection<PartSchematicResponse>?>
{
}
