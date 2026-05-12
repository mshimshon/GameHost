using GameHost.Features.Mods.Domain.ValueObjects;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

internal sealed record GetAllModListQuery : IRequest<ICollection<ModListDescriptor>>
{
}
