using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.LinuxGameServer.Web.Components.ViewModels;

public interface ISetupProcessViewModel : IWidgetViewModel
{
    public InstallationState InstallState { get; }
    public GameManifestResponse KeyGame { get; set; }
    public string RepositoryTarget { get; }
    DateTime LastUpdate { get; }
    Task InstallAsync();
}
