using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Web.Hooks.Events.Features.LinuxGameServer.Dto;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Messaging.EventBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Hooks.Events;

[EventBusId(LinuxGameServerKeys.Events.ON_GAME_SERVER_INSTALL_STATE_CHANGED)]
internal class WhenServerInstallationStateChanged : IEventBusHandler
{
    private readonly IDispatcher _dispatcher;
    private readonly IStateAccessor<ModListState> _modListState;
    private readonly ICrazyReport _crazyReport;

    public WhenServerInstallationStateChanged(IDispatcher dispatcher,
        IStateAccessor<ModListState> modListState,
        ICrazyReport<WhenServerInstallationStateChanged> crazyReport)
    {
        _dispatcher = dispatcher;
        _modListState = modListState;
        _crazyReport = crazyReport;
        crazyReport.SetModule(ModListKeys.MODULE_NAME);
    }
    public async Task HandleAsync(IEventBusMessage evt)
    {
        var state = await evt.ReadAs<InstallationStateResponse>();
        if (!state.IsInstallationCompleted) return;
        await Task.WhenAll(
            LoadModListSchematic(state),
            LoadModFeature(state)
        );
    }

    private Task LoadModListSchematic(InstallationStateResponse state)
    {
        if (!_modListState.State.FeatureInfo?.IsEnabled ?? false)
            return Task.CompletedTask;
        if (!_modListState.State.IsSchematicPartsLoading && !_modListState.State.IsSchematicPartsLoaded)
            return _dispatcher.Prepare<LoadModListSchematicAction>().DispatchAsync();
        return Task.CompletedTask;
    }

    private Task LoadModFeature(InstallationStateResponse state)
    {
        if (!_modListState.State.IsFeatureInfoLoading && _modListState.State.FeatureInfo == default)
            return _dispatcher.Prepare<LoadModFeatureAction>().DispatchAsync();
        return Task.CompletedTask;
    }
}