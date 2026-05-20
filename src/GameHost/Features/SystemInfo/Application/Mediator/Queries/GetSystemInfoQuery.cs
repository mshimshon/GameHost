using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using MedihatR;

namespace GameHost.Features.SystemInfo.Application.Mediator.Queries;

public record GetSystemInfoQuery : IRequest<SystemInfoResponse?>
{
}
