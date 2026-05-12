using GameHost.Features.LinuxGameServer.Domain.Entities;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries;

public sealed record GetInstalledGameQuery : IRequest<GameServerInfoEntity?>
{
}
