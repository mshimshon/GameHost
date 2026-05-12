using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Mediator.Queries;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Effects;

internal class UpdateInstalledGameServerEffect : IEffect<UpdateInstalledGameServerAction>
{
    private readonly IMedihater _medihater;
    private readonly ICrazyReport<UpdateInstalledGameServerEffect> _crazyReport;

    public UpdateInstalledGameServerEffect(IMedihater medihater, ICrazyReport<UpdateInstalledGameServerEffect> crazyReport)
    {
        _medihater = medihater;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
    }
    public async Task EffectAsync(UpdateInstalledGameServerAction action, IDispatcher dispatcher)
    {
        var result = await _medihater.Send(new GetInstalledGameQuery());
        _crazyReport.Report("Fetched Installation Info = {0}", result?.ToString() ?? "null");
        await dispatcher.Prepare<UpdateInstalledGameServerDoneAction>()
            .With(p => p.GameServerInfo, result)
            .DispatchAsync();
    }
}
