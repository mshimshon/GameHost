using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameInfo;

namespace GameHost.Features.Lifecycle.Application.Services;

public interface IGameInfoService
{
    Task<string?> GetRawGameInfoAsync(CancellationToken ct = default);
    Task<GameInfoResponse?> LoadGameInfoAsync(CancellationToken ct = default);

}
