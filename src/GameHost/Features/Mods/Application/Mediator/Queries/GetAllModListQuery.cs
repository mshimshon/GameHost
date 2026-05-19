using GameHost.Features.Mods.Application.Payloads.Responses;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

internal sealed record GetAllModListQuery : IRequest<ICollection<ModListDescriptorResponse>>
{
}
