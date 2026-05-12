namespace GameHost.Kernel.Abstractions.Services.ActionFileWatcher;

/// <summary>
/// if use other circuit (startup), only use on global state or internal state otherwise only use on circuit.
/// </summary>
public interface IStateFileWatcher<TAction> : IDisposable where TAction : FileWatchActionBase
{
    string Directory { get; init; }
    string FilePattern { get; init; }

}
