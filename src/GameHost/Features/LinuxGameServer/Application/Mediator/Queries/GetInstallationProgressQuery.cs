using GameHost.Features.LinuxGameServer.Application.Models;
using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Queries;

public sealed record GetInstallationProgressQuery : IRequest<GameServerInstallProcessModel>
{
}
