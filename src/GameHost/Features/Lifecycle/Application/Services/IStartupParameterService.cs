using GameHost.Features.Lifecycle.Infrastructure.Services.Contracts;

namespace GameHost.Features.Lifecycle.Application.Services;

public interface IStartupParameterService
{
    Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken ct = default);
    Task<ICollection<GameStartupParameterHostDefResponse>> GetHostServerStartupParametersAsync(CancellationToken cancellationToken = default);
    Task UpdateStartupParameterAsync(string key, string value, CancellationToken ct = default);
}
