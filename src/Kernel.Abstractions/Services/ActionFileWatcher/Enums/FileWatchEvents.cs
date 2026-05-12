namespace GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;

public enum FileWatchEvents
{
    Created = 0,
    Removed = 1,
    Updated = 2,
    Renamed = 3,
    Any = 4
}