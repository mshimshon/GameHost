using GameHost.Features.SystemInfo.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.States;

public record SystemInfoState : IStateFeatureSingleton
{
    public SystemInfoEntity? SystemInfo { get; init; }
    public DateTime LastUpdate { get; init; }

}
