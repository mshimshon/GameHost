using GameHost.Features.SystemInfo.Application.Payloads.Responses;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.SystemInfo.Web.Components.ViewModels;

public interface ISystemResourcesStatusViewModel : IWidgetViewModel
{
    SystemInfoResponse? SystemInfo { get; }
    DateTime LastUpdate { get; }
}
