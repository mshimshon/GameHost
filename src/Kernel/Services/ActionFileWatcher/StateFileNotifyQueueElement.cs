using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;

namespace GameHost.Kernel.Services.ActionFileWatcher;

internal sealed record StateFileNotifyQueueElement
{
    public string Path { get; init; } = string.Empty;
    public long Version { get; init; }
    public FileWatchEvents EventType { get; init; }
    public Func<Task> Action { get; init; } = default!;
}
