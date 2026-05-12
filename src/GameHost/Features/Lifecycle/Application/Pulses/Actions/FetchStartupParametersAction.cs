using GameHost.Kernel.Abstractions.Services.ActionFileWatcher;

namespace GameHost.Features.Lifecycle.Application.Pulses.Actions;

internal sealed record FetchStartupParametersAction : FileWatchActionBase
{
}
