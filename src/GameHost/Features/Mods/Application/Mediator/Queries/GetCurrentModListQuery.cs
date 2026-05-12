using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

internal sealed record GetCurrentModListQuery : IRequest<Guid?>
{
}
