using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries;

public sealed record GetRawGameInfoQuery : IRequest<string?>
{
}
