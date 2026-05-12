using GameHost.Features.SystemInfo.Domain.Entites;
using MedihatR;

namespace GameHost.Features.SystemInfo.Application.CQRS.Queries;

public record GetSystemInfoQuery : IRequest<SystemInfoEntity?>
{
}
