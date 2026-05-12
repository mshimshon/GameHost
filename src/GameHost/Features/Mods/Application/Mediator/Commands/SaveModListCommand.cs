using GameHost.Features.Mods.Domain.Entities;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands;

internal sealed record SaveModListCommand(ModListEntity ModListEntity) : IRequest;
