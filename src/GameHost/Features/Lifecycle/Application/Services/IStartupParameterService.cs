
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

namespace GameHost.Features.Lifecycle.Application.Services;

public interface IStartupParameterService
{
    Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken ct = default);
    Task<ICollection<GameConfigParameterPairHostResponse>> GetHostServerStartupParametersAsync(CancellationToken cancellationToken = default);
    Task UpdateStartupParameterAsync(string key, string value, CancellationToken ct = default);
}
