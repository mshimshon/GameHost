using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands;

internal sealed record UpdateCurrentModlistCommand(Guid? Id) : IRequest { }
