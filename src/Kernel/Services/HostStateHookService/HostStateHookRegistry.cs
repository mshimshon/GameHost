using GameHost.Kernel.Abstractions.Services.HostStateHookService;

namespace GameHost.Kernel.Services.HostStateHookService;

public class HostStateHookRegistry : IHostStateHookRegistry, IDisposable
{
    public Dictionary<BindIdentifier, StateAccessorDescriptor> Accessors { get; set; } = new();
    public Dictionary<Type, List<Func<Task>>> GarantueedCallbacks { get; set; } = new();

    private readonly object _lock = new object();
    private bool _disposedValue;

    public void Register(StateAccessorDescriptor stateAccessorDescriptor)
    {
        var identity = stateAccessorDescriptor.Identifier;
        lock (_lock)
        {
            if (Accessors.ContainsKey(identity))
            {
                Accessors[identity].Count++;
                return;
            }
            Accessors.Add(identity, stateAccessorDescriptor);
            stateAccessorDescriptor.Accessor.OnStateChangedNoDetails += stateAccessorDescriptor.HandlerCallback!;
            stateAccessorDescriptor.GetGarantueedCallbacks = GetReadOnlyGarantueedCallbacks;
        }
    }

    public void DecountOrRemove(BindIdentifier identifier)
    {
        lock (_lock)
        {
            if (!Accessors.ContainsKey(identifier)) return;
            Accessors[identifier].Count--;
            if (Accessors[identifier].Count <= 0)
                Accessors.Remove(identifier);
        }
    }
    public void RegisterCallback(Type state, Func<Task> clbk)
    {
        if (!GarantueedCallbacks.ContainsKey(state))
            GarantueedCallbacks.Add(state, new());
        GarantueedCallbacks[state].Add(clbk);
    }

    public void UnRegisterCallback(Type state, Func<Task> clbk)
    {
        if (!GarantueedCallbacks.ContainsKey(state)) return;
        GarantueedCallbacks[state].Remove(clbk);
    }
    public IReadOnlyDictionary<Type, IReadOnlyList<Func<Task>>> GetReadOnlyGarantueedCallbacks()
    {
        lock (_lock)
        {
            var shadowList = GarantueedCallbacks
            .ToDictionary(p => p.Key, p => (IReadOnlyList<Func<Task>>)p.Value.ToList().AsReadOnly())
            .AsReadOnly();
            return shadowList;
        }

    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                lock (_lock)
                {
                    foreach (var item in Accessors.Values)
                    {
                        item.Accessor.OnStateChangedNoDetails -= item.HandlerCallback!;
                    }
                }
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}