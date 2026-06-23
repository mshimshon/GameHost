using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Web.Hooks.Queries;

[QueryBusKey(LinuxGameServerKeys.Queries.IS_GAME_SERVER_INSTALLED)]
internal class IsGameInstalledHook : IQueryBusHandler
{
    private readonly IStateAccessor<InstallationState> _gameInstallState;
    public IsGameInstalledHook(IStateAccessor<InstallationState> gameInstallState)
    {
        _gameInstallState = gameInstallState;
    }

    public Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
        => qry.ReplyWith(_gameInstallState.State.InstalledGameServer != default);
}
