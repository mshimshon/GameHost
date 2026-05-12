using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Mediator.Commands;
using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using MedihatR;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Application.Pulses.Effects;

public record InstallGameServerActionEffect : IEffect<InstallGameServerAction>
{
    private readonly IMedihater _medihater;
    private readonly ICrazyReport _crazyReport;

    public InstallGameServerActionEffect(IMedihater medihater, ICrazyReport crazyReport)
    {
        _medihater = medihater;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<InstallGameServerActionEffect>(LinuxGameServerKeys.MODULE_NAME);
    }
    public async Task EffectAsync(InstallGameServerAction action, IDispatcher dispatcher)
    {
        try
        {
            await _medihater.Send(new InstallGameServerCommand(action.GameManifest.Id, action.GameManifest.InstallerName));
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);
            await dispatcher.Prepare<GameServerInstallFailedAction>()
                .With(p => p.Id, action.GameManifest.Id)
                .With(p => p.DisplayName, action.GameManifest.DisplayName)
                .With(p => p.FailureReason, "Cannot trigger installation script for some reasons") //TODO: Localize
                .DispatchAsync();
        }

    }
}
