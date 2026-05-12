using Microsoft.Extensions.DependencyInjection;
using StatePulse.Net;

namespace GameHost.Kernel.Abstractions.Extensions;

public static class StatePulseServiceExt
{
    /// <summary>
    /// Dispatch and Await SP Action (WARNING: Does not support constructor nor setup of parameters)
    /// </summary>
    public static async Task DispatchAction<TAction>(this IServiceProvider serviceProvider) where TAction : IAction
    {
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        // TODO: WORKAROUND SP ISSUE OF RELAYING TACTION, REMOVE WHEN FIXED BY SP TEAM
        // Actual Issue is Roslyn Analyzer trigger error when relaying action. TAction to TAction
        //await dispatcher.Prepare<TAction>().Await().DispatchAsync();

        var action = Activator.CreateInstance<TAction>();
        await dispatcher.Prepared(action).Await().DispatchAsync();
    }
}
