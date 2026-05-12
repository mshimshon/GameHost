using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;

namespace GameHost.Kernel.Services.ActionFileWatcher;

internal sealed record FileTrackerKey
{

    public string FilePath { get; }
    public FileWatchEvents FileWatchEvents { get; }

    public FileTrackerKey(string filePath, FileWatchEvents fileWatchEvents)
    {
        FilePath = filePath;
        FileWatchEvents = fileWatchEvents;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FilePath, FileWatchEvents);
    }
}
