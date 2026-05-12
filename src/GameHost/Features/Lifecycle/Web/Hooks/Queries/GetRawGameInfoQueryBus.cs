using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Mediator.Queries;
using MedihatR;

namespace GameHost.Features.Lifecycle.Web.Hooks.Queries;

[QueryBusId(LifecycleKeys.Queries.GET_RAW_GAME_INFO)]
internal class GetRawGameInfoQueryBus : IQueryBusHandler
{
    private readonly IMedihater _medihater;

    public GetRawGameInfoQueryBus(IMedihater medihater)
    {
        _medihater = medihater;
    }
    public async Task<QueryBusMessageResponse> HandleAsync(IQueryBusMessage qry)
    {
        var query = new GetRawGameInfoQuery();
        var result = await _medihater.Send(query);
        return await qry.ReplyWith(result);
    }
}
