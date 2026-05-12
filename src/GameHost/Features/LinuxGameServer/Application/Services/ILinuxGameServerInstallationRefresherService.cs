namespace GameHost.Features.LinuxGameServer.Application.Services;

public interface ILinuxGameServerInstallationRefresherService
{
    bool IsRunning { get; }
    Task MonitorProgressFileAsync(CancellationToken cancellationToken = default);
    Task StopMonitoringFileAsync();
}
