using GameHost.Features.SystemInfo.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.Actions;

public record SystemInfoUpdatedAction : IAction
{
    public SystemInfoEntity SystemInfo { get; set; } = default!;
}
