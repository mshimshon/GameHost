using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Domain.Entites;

namespace GameHost.Features.Lifecycle.Web.Components.ViewModels;

public interface IStartupParameterViewModel : IWidgetViewModel
{
    public GameInfoEntity? GameInfo { get; }
    public Dictionary<string, string> StartupParameters { get; }
    public bool SavedParametersLoaded { get; }
    Task GroupingParameters();
    Dictionary<string, List<GameConfigParamaterEntity>> Parameters { get; }
    string GetInitialValue(GameConfigParamaterEntity parameter);
    ServerState ServerState { get; }
}

