using GameHost.Features.Lifecycle.Domain.Entites;
using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries;

public sealed record GetGameInfoQuery : IRequest<GameInfoEntity?>
{
}
