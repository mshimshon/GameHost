using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;
using GameHost.Features.Lifecycle.Application.Pulses.States;
using GameHost.Features.Lifecycle.Web.Components.ViewModels;
using LunaticPanel.Core.Abstraction.Widgets;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;
using System.Data;

namespace GameHost.Features.Lifecycle.Web.Components;

public class StartupParameterViewModel : WidgetViewModelBase, IStartupParameterViewModel
{
    private readonly IStatePulse _statePulse;
    private readonly ICrazyReport _crazyReport;

    public StartupParameterViewModel(IStatePulse statePulse, ICrazyReport crazyReport)
    {
        _statePulse = statePulse;
        _crazyReport = crazyReport;
        _crazyReport.SetModule<StartupParameterViewModel>(LifecycleKeys.MODULE_NAME);
        _ = GroupingParameters();
    }

    public ServerState ServerState => _statePulse.StateOf<ServerState>(() => this, UpdateChanges);

    public GameInfoState GameInfoState => _statePulse.StateOf<GameInfoState>(() => this, UpdateChanges);
    public Dictionary<string, List<GameConfigParameterResponse>> Parameters { get; private set; } = new();

    public GameInfoResponse? GameInfo => GameInfoState.GameInfo;

    public Dictionary<string, string> StartupParameters => GameInfoState.StartupParameters;

    public bool SavedParametersLoaded => GameInfoState.SavedParametersLoaded;

    protected override async Task OnViewModelBeforeRenderAsync()
    {
        await GroupingParameters();

    }
    public Task GroupingParameters()
    {
        Parameters = GameInfoState.GameInfo?.StartupParameters != default ? GameInfoState.GameInfo.StartupParameters
            .Where(p => !string.IsNullOrEmpty(p.Category))
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.ToList())
            : new();
        return Task.CompletedTask;
    }

    public string GetInitialValue(GameConfigParameterResponse parameter) =>
        StartupParameters.ContainsKey(parameter.Key) ?
        StartupParameters[parameter.Key] :
        parameter.DefaultValue ?? string.Empty;


}
