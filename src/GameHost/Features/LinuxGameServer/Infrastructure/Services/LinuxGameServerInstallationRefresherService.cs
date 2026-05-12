using GameHost.Features.LinuxGameServer.Application.Pulses.Actions;
using GameHost.Features.LinuxGameServer.Application.Services;
using StatePulse.Net;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerInstallationRefresherService : ILinuxGameServerInstallationRefresherService
{
    private bool _isRefreshingInstallationProgress;

    private readonly object _lock = new();
    private readonly IDispatcher _dispatcher;

    public bool IsRunning => _isRefreshingInstallationProgress;

    public LinuxGameServerInstallationRefresherService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    public Task MonitorProgressFileAsync(CancellationToken cancellationToken = default)
    {

        lock (_lock)
        {
            if (_isRefreshingInstallationProgress) return Task.CompletedTask;
            _isRefreshingInstallationProgress = true;
        }
        _ = RunMonitorLoop(cancellationToken);
        return Task.CompletedTask;
    }
    public async Task RunMonitorLoop(CancellationToken cancellationToken = default)
    {
        do
        {
            await _dispatcher.Prepare<UpdateProgressStateFromDiskAction>().Await().DispatchAsync();
            await _dispatcher.Prepare<UpdateInstalledGameServerAction>().Await().DispatchAsync();
            await Task.Delay(500);
        } while (_isRefreshingInstallationProgress);
    }

    public Task StopMonitoringFileAsync()
    {
        lock (_lock)
        {
            _isRefreshingInstallationProgress = false;
        }
        return Task.CompletedTask;
    }
}
