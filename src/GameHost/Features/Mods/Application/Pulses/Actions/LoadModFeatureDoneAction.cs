using GameHost.Features.Mods.Application.Contracts.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.Actions;

public sealed record LoadModFeatureDoneAction : IAction
{
    public ModFeatureResponse? ModFeature { get; set; }
}
