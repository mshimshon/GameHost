using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Web.Hooks.Queries;

[QueryBusKey(LinuxGameServerKeys.Queries.GET_SERVER_INSTALL_STATE)]
internal class GetInstallationStateHook : IQueryBusHandler
{
    private readonly IStateAccessor<InstallationState> _gameInstallState;
    public GetInstallationStateHook(IStateAccessor<InstallationState> gameInstallState)
    {
        _gameInstallState = gameInstallState;
    }

    public Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
        => qry.ReplyWith(_gameInstallState.State);
}
