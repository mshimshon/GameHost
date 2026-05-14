using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record ServerGameInfoUpdateDoneAction : IAction
{
    public GameInfoResponse GameInfo { get; set; } = default!;
}
