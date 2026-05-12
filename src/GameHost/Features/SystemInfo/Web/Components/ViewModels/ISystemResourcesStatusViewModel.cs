using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.SystemInfo.Domain.Entites;

namespace GameHost.Features.SystemInfo.Web.Components.ViewModels;

public interface ISystemResourcesStatusViewModel : IWidgetViewModel
{
    SystemInfoEntity? SystemInfo { get; }
    DateTime LastUpdate { get; }
}
