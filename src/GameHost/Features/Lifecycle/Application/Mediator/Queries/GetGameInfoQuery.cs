using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;
using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries;

public sealed record GetGameInfoQuery : IRequest<GameInfoResponse?>
{
}
