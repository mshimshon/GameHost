using GameHost.Features.Mods.Application.Payloads.Responses;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Commands;

internal sealed record SaveModListCommand(ModListResponse ModList) : IRequest;
