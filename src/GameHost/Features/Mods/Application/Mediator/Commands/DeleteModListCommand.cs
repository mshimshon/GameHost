using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands;

internal sealed record DeleteModListCommand(Guid Id) : IRequest;