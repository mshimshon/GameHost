using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record UpdateStartupParameterAction : ISafeAction
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
