using GameHost.Kernel.Abstractions.Services.ActionFileWatcher;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Actions;

public sealed record UpdateInstalledGameServerAction : FileWatchActionBase
{
}
