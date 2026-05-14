using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Lifecycle.Web.Components.ViewModels;

public interface IStartupParameterViewModel : IWidgetViewModel
{
    public GameInfoResponse? GameInfo { get; }
    public Dictionary<string, string> StartupParameters { get; }
    public bool SavedParametersLoaded { get; }
    Task GroupingParameters();
    Dictionary<string, List<GameConfigParameterResponse>> Parameters { get; }
    string GetInitialValue(GameConfigParameterResponse parameter);
    ServerState ServerState { get; }
}

