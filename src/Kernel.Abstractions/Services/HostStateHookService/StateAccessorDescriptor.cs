using StatePulse.Net;

namespace GameHost.Kernel.Abstractions.Services.HostStateHookService;

public sealed record StateAccessorDescriptor
{
    public IStateAccessor Accessor { get; init; } = default!;
    public Func<Task> Reaction { get; init; } = default!;
    public BindIdentifier Identifier { get; init; } = default!;
    public Func<IReadOnlyDictionary<Type, IReadOnlyList<Func<Task>>>> GetGarantueedCallbacks { get; set; } = default!;
    public int Count { get; set; } = 1;

    public void HandlerCallback(object _, EventArgs e)
    {
        Reaction?.Invoke();
        var callbacks = GetGarantueedCallbacks();
        if (callbacks.ContainsKey(Identifier.StateType))
            foreach (var cbk in callbacks[Identifier.StateType])
                cbk?.Invoke();
    }
}