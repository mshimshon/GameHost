using GameHost.Features.Lifecycle.Domain.Entites;
using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries;

public record GetServerStatusQuery : IRequest<ServerInfoEntity?>;
