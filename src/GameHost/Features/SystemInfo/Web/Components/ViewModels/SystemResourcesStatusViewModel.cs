using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.SystemInfo.Application.Pulses.States;
using GameHost.Features.SystemInfo.Domain.Entites;
using StatePulse.Net;

namespace GameHost.Features.SystemInfo.Web.Components.ViewModels;

public class SystemResourcesStatusViewModel : WidgetViewModelBase, ISystemResourcesStatusViewModel
{

    private readonly IStatePulse _statePulse;

    public SystemInfoState SystemState => _statePulse.StateOf<SystemInfoState>(() => this, UpdateState);

    public SystemInfoEntity? SystemInfo => SystemState.SystemInfo;
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
