using GameHost.Kernel.Abstractions.Services.Notification.Enums;
using StatePulse.Net;

namespace GameHost.Features.Notification.Application.Pulses.Actions;

public sealed record SendToastNotificationAction : IAction
{
    public string Message { get; set; } = default!;
    public NotificationSeverity Color { get; set; } = NotificationSeverity.Info;
}
