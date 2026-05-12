using GameHost.Features.Mods.Domain.Entities;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

internal sealed record GetModListQuery(Guid Id) : IRequest<ModListEntity>;
