using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.States;

public record SystemInfoState : IStateFeatureSingleton
{
    public SystemInfoResponse? SystemInfo { get; init; }
    public DateTime LastUpdate { get; init; }

}
