using GameHost.Features.Lifecycle.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record ServerGameInfoUpdateDoneAction : IAction
{
    public GameInfoEntity GameInfo { get; set; } = default!;
}
