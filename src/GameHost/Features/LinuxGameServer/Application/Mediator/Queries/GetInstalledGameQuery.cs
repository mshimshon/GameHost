using GameHost.Features.LinuxGameServer.Application.Payloads.Responses;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries;

public sealed record GetInstalledGameQuery : IRequest<GameServerInfoResponse?>
{
}
