using GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;
using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries;

public record GetServerStatusQuery : IRequest<ServerInfoResponse?>;
