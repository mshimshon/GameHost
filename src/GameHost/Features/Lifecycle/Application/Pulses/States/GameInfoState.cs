using GameHost.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.States;

public record GameInfoState : IStateFeatureSingleton
{
    public GameInfoEntity? GameInfo { get; init; }
    public Dictionary<string, string> StartupParameters { get; init; } = new();
    public bool SavedParametersLoaded { get; init; }
}

/*
 * Get the GameInfo -> Mod Parts
 * QueryBus -> Get GameInfo Raw Json 
 * 
 */