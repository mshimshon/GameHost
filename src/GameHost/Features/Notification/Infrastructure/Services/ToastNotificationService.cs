using GameHost.Features.Notification.Application.Pulses.Actions;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using GameHost.Kernel.Abstractions.Services.Notification.Services;
using LunaticPanel.Core.Abstraction.Circuit;
using StatePulse.Net;

namespace GameHost.Features.Notification.Infrastructure.Services;

internal sealed class ToastNotificationService : INotificationService
{
    private readonly IDispatcher _dispatcher;
    private readonly ICircuitRegistry _circuitRegistry;

    public ToastNotificationService(IDispatcher dispatcher, ICircuitRegistry circuitRegistry)
    {
        _dispatcher = dispatcher;
        _circuitRegistry = circuitRegistry;
    }
    public async Task NotifyAsync(string message, NotificationSeverity severity)
    {
        if (_circuitRegistry.CurrentCircuit == default || _circuitRegistry.CurrentCircuit.IsMaster)
            return;
        await _dispatcher.Prepare<SendToastNotificationAction>()
        .With(p => p.Message, message)
        .With(p => p.Color, severity)
        .DispatchAsync();
    }

    public async Task NotifyAsync(WebServiceException ex)
    {
        // TODO: LOCALIZE ERR CODES
        await NotifyAsync(ex.Message, NotificationSeverity.Error);
    }
}
