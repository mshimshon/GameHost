using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Web.Components.ViewModels;

public class SystemResourcesStatusViewModel : WidgetViewModelBase, ISystemResourcesStatusViewModel
{

    private readonly IStatePulse _statePulse;

    public SystemInfoState SystemState => _statePulse.StateOf<SystemInfoState>(() => this, UpdateState);

    public SystemInfoResponse? SystemInfo => SystemState.SystemInfo;
    public DateTime LastUpdate => SystemState.LastUpdate;
    public SystemResourcesStatusViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }
    public async Task UpdateState()
    {
        await UpdateChanges();
    }

}
