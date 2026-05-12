namespace GameHost.Kernel.Abstractions.Services.HostStateHookService;

public interface IHostStateHookRegistry
{
    void Register(StateAccessorDescriptor stateAccessorDescriptor);
    void DecountOrRemove(BindIdentifier identifier);
    void RegisterCallback(Type state, Func<Task> clbk);
    void UnRegisterCallback(Type state, Func<Task> clbk);
    IReadOnlyDictionary<Type, IReadOnlyList<Func<Task>>> GetReadOnlyGarantueedCallbacks();
}
