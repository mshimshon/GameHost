using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;
using StatePulse.Net;

namespace GameHost.Kernel.Abstractions.Services.ActionFileWatcher;

public abstract record FileWatchActionBase : ISafeAction
{
    public DateTime Date { get; set; }
    public FileWatchEvents Event { get; set; }
    public string? FullName { get; set; }
    public string? FileName { get; set; }
}
