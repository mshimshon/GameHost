using GameHost.Features.Mods.Application.Contracts.Responses;
using MedihatR;

namespace GameHost.Features.Mods.Application.Mediator.Queries;

internal sealed record GetModFeatureQuery : IRequest<ModFeatureResponse?>
{
}
