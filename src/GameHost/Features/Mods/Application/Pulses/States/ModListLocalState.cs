using GameHost.Features.Mods.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.States;

public record ModListLocalState : IStateFeature
{
    public ModListResponse? Current { get; init; }
    public bool IsCurrentLoading { get; init; }

    public bool IsCreationLoading { get; init; }
    public bool DidLastCreationFailed { get; init; }

}
