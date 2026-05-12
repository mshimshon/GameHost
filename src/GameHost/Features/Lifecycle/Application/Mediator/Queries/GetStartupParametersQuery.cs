using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Queries;

public record GetStartupParametersQuery : IRequest<Dictionary<string, string>>;
