using GameHost.Kernel.Abstractions.Services.ActionFileWatcher;
using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;
using GameHost.Kernel.Services.ActionFileWatcher;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;
using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Kernel.Extensions;

public static class StateFileWatchServiceExt
{
    public static void AddMasterStateFileWatcherService<TAction>(this IServiceCollection services, Func<IPluginLocation, string> getBasePath, string filePattern, FileWatchEvents[] whatToWatch) where TAction : FileWatchActionBase
    {
        services.AddSingleton<IStateFileWatcher<TAction>>(sp => AddActionService<TAction>(sp, getBasePath.Invoke(sp.GetRequiredService<IPluginLocation>()), filePattern, whatToWatch));
    }

    static StateFileWatcher<TAction> AddActionService<TAction>(IServiceProvider serviceProvider, string folder, string filePattern, FileWatchEvents[] whatToWatch)
        where TAction : FileWatchActionBase
        => new StateFileWatcher<TAction>(folder, filePattern, whatToWatch, serviceProvider.GetRequiredService<IDispatcher>(), serviceProvider.GetRequiredService<ICrazyReport<TAction>>());

    public static IStateFileWatcher<TAction> LoadWatcher<TAction>(this IServiceProvider sp)
        where TAction : FileWatchActionBase => sp.GetRequiredService<IStateFileWatcher<TAction>>();
}
