using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Lifecycle.Web.Components.ViewModels;

public interface IServerControlViewModel : IWidgetViewModel
{
    ServerState ServerState { get; }
    GameInfoState GameInfoState { get; }
    GameInfoEntity? GameInfo { get; }
    SystemInfoState SystemInfoState { get; }
    ServerTransitionState TransitionState { get; }
    Task Start();
    Task UpdateState();
    Task Stop();
    bool IsRunning();
    bool IsStopped();
    bool IsRestarting();
    bool IsFailed();
    bool IsWaiting();
    Task StartCaringAboutTransitionAsync();
    Task StopCaringAboutTransitionAsync();
    Guid GetPanelId();
}
