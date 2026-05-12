using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.States;

public record GameRepositoryState : IStateFeatureSingleton
{
    public bool IsInitializing { get; init; }
    public string? ErrorMessage { get; init; }
    public bool HasFailed => ErrorMessage != default;
    public bool Completed { get; init; }

}
