using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands;

internal sealed record CreateModListCommand(Guid Id, string Name) : IRequest;
