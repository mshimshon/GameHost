using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using GameHost.Features.LinuxGameServer.Web.Hooks.UI.Components;
using LunaticPanel.Core.Abstraction.Messaging.EngineBus;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Engine.Keys.UI;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Web.Hooks.UI;

[EngineBusKey(DashboardKeys.UI.GetWidgets)]
public class WidgetServerSetupHook : IEngineBusHandler
{
    private readonly IStateAccessor<InstallationState> _installStateAccess;

    public WidgetServerSetupHook(IStateAccessor<InstallationState> installStateAccess)
    {
        _installStateAccess = installStateAccess;
    }
    public Task<EngineBusResponse> HandleAsync(IEngineBusMessage engineBusMessage)
        => engineBusMessage.ReplyWithTypeOf<WidgetServerSetup>((msg) => msg with
        {
            VisibilityCondition = () => !_installStateAccess.State.IsInstallationCompleted
        });
}
