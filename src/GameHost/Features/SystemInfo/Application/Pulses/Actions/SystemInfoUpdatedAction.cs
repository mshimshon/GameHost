using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Application.Pulses.Actions;

public record SystemInfoUpdatedAction : IAction
{
    public SystemInfoResponse SystemInfo { get; set; } = default!;
}
