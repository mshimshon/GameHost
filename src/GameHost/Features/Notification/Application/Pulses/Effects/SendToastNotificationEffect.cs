using GameHost.Features.Notification.Application.Pulses.Actions;
using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StatePulse.Net;

namespace GameHost.Features.Notification.Application.Pulses.Effects;

public class SendToastNotificationEffect : IEffect<SendToastNotificationAction>
{
    private readonly ISnackbar _snackbar;

    public SendToastNotificationEffect(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }
    public Task EffectAsync(SendToastNotificationAction action, IDispatcher dispatcher)
    {

        Severity selectedSeverity = action.Color switch
        {
            NotificationSeverity.Error => Severity.Error,
            NotificationSeverity.Warning => Severity.Warning,
            NotificationSeverity.Success => Severity.Success,
            _ => Severity.Info
        };
        _snackbar.Add((MarkupString)action.Message, selectedSeverity);

        return Task.CompletedTask;
    }
}
