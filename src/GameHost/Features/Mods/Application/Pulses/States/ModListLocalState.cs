using GameHost.Features.Mods.Domain.Entities;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.States;

public record ModListLocalState : IStateFeature
{
    public ModListEntity? Current { get; init; }
    public bool IsCurrentLoading { get; init; }

    public bool IsCreationLoading { get; init; }
    public bool DidLastCreationFailed { get; init; }

}
