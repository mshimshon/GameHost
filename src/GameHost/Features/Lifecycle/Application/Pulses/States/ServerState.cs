using GameHost.Features.Lifecycle.Application.Pulses.States.Enums;
using GameHost.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.States;

public record ServerState : IStateFeatureSingleton
{
    public ServerInfoEntity? ServerInfo { get; init; }
    public ServerTransition Transition { get; init; } = ServerTransition.Idle;
    public DateTime TransitionStartedAt { get; init; }

    public DateTime ServerInfoLastUpdate { get; init; }
    public string? LastRunErrorCode { get; init; }
    public string? LastRunErrorMessage { get; init; }
    public int SkipNextUpdates { get; init; }
    public int Delay { get; init; } = 8;
}
