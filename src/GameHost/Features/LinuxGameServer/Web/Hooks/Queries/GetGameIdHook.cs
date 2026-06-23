using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Messaging.Common;
using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Web.Hooks.Queries;

[QueryBusKey(LinuxGameServerKeys.Queries.GET_GAME_ID, ServiceLifetime = EBusLifetime.Scoped)]
internal class GetGameIdHook : IQueryBusHandler
{
    private readonly IStateAccessor<InstallationState> _installlationStateAccess;

    public GetGameIdHook(IStateAccessor<InstallationState> installlationStateAccess)
    {
        _installlationStateAccess = installlationStateAccess;
    }
    public async Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
    {
        if (_installlationStateAccess.State.InstalledGameServer == default)
            return await qry.ReplyWith(null);
        return await qry.ReplyWith(_installlationStateAccess.State.InstalledGameServer.Id);
    }
}
