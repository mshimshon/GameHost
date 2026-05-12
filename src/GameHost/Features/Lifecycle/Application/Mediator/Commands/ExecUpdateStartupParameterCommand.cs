using MedihatR;

namespace GameHost.Features.Lifecycle.Application.Mediator.Commands;

public record ExecUpdateStartupParameterCommand : IRequest
{
    public string Key { get; init; } = default!;
    public string Value { get; init; } = default!;
}
