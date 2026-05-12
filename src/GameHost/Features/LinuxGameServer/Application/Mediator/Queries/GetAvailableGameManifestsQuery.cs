using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries;

public sealed record GetAvailableGameManifestsQuery : IRequest<ICollection<GameManifestResponse>?>
{
}


// Infrastructure Layer (Contracts/Return Entities) -> Application (Input Entities, Return Contracts) 