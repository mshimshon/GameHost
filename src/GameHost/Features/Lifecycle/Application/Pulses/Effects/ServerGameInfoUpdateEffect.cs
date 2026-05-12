using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Mediator.Queries;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Application.Pulses.Effects;

internal class ServerGameInfoUpdateEffect : IEffect<ServerGameInfoUpdateAction>
{
    private readonly IMedihater _medihater;
    private readonly ICrazyReport _crazyReport;

    public ServerGameInfoUpdateEffect(IMedihater medihater, ICrazyReport crazyReport)
    {
        _medihater = medihater;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<ServerGameInfoUpdateEffect>(LifecycleKeys.MODULE_NAME);
    }
    public async Task EffectAsync(ServerGameInfoUpdateAction action, IDispatcher dispatcher)
    {
        var exec = new GetGameInfoQuery();
        var gameInfo = await _medihater.Send(exec);
        _crazyReport.ReportInfo("gameInfo is {0}", gameInfo?.ToString() ?? null);
        if (gameInfo != default)
        {
            //TODO: MONITOR CONSISTENCY OF STATE AS TWO ACTION CHANGES THE SAME STATE DIFFERENT PROPS.
            await dispatcher.Prepare<ServerGameInfoUpdateDoneAction>()
                .With(p => p.GameInfo, gameInfo)
                .DispatchAsync();
            await dispatcher.Prepare<FetchStartupParametersAction>()
                .DispatchAsync();
        }

    }
}
