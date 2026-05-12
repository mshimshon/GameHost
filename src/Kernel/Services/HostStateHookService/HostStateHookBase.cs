using LunaticPanel.Core.Abstraction.Tools;
using GameHost.Kernel.Abstractions.Services.HostStateHookService;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Kernel.Services.HostStateHookService;

public abstract class HostStateHookBase : IDisposable
{
    private bool _disposedValue;
    protected readonly IPanelControl _panelControl;
    private readonly IHostStateHookRegistry _hostStateHookRegistry;
    private readonly IServiceProvider _serviceProvider;

    private List<BindIdentifier> _localRegistry = new();
    private Dictionary<Type, IStateAccessor> _accessorRegistryCache = new Dictionary<Type, IStateAccessor>();
    private Dictionary<Type, List<Func<Task>>> _localCallback = new();
    public HostStateHookBase(IPanelControl panelControl, IHostStateHookRegistry hostStateHookRegistry, IServiceProvider serviceProvider)
    {
        _panelControl = panelControl;
        _hostStateHookRegistry = hostStateHookRegistry;
        _serviceProvider = serviceProvider;
    }

    protected void SetStateCallback<TState>(Func<Task> onGarantueedUpdate)
    {
        var type = typeof(TState);
        _hostStateHookRegistry.RegisterCallback(type, onGarantueedUpdate);
        if (!_localCallback.ContainsKey(type))
            _localCallback.Add(type, new());
        _localCallback[type].Add(onGarantueedUpdate);
    }

    protected void HostReactToState<TState>(string hostRenderMethodName)
    {

        var accessor = _serviceProvider.GetRequiredService<IStateAccessor<TState>>();
        var item = new StateAccessorDescriptor()
        {
            Accessor = accessor,
            Identifier = new BindIdentifier(typeof(TState), hostRenderMethodName),
        };

        switch (hostRenderMethodName)
        {
            case nameof(IPanelControl.MenuRender):
                item = item with
                {
                    Reaction = _panelControl.MenuRender
                };
                break;
            case nameof(IPanelControl.LayoutRender):
                item = item with
                {
                    Reaction = _panelControl.LayoutRender
                };
                break;
            case nameof(IPanelControl.DashboardRender):
                item = item with
                {
                    Reaction = _panelControl.DashboardRender
                };
                break;
            default:
                return;
        }
        _hostStateHookRegistry.Register(item);
        if (!_accessorRegistryCache.ContainsKey(item.Identifier.StateType))
            _accessorRegistryCache.Add(item.Identifier.StateType, accessor);
        if (!_localRegistry.Contains(item.Identifier))
            _localRegistry.Add(item.Identifier);
    }

    protected TState? GetState<TState>() where TState : class, IStateFeature
    {

        if (!_accessorRegistryCache.ContainsKey(typeof(TState)))
            return default;
        return _accessorRegistryCache[typeof(TState)].GetAs<TState>();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                foreach (var item in _localRegistry)
                    _hostStateHookRegistry.DecountOrRemove(item);

                foreach (var item in _localCallback)
                    foreach (var action in item.Value)
                        _hostStateHookRegistry.UnRegisterCallback(item.Key, action);
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}