namespace GameHost.Features.Lifecycle.Application.Services;

public interface ILifecycleServices
{
    Task ServerStartAsync(CancellationToken ct = default);
    Task ServerStopAsync(CancellationToken ct = default);
    Task ServerRestartAsync(CancellationToken ct = default);
    Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken ct = default);

}
