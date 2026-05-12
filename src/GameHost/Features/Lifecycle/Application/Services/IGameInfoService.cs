using GameHost.Features.Lifecycle.Domain.Entites;

namespace GameHost.Features.Lifecycle.Application.Services;

public interface IGameInfoService
{
    Task<string?> GetRawGameInfoAsync(CancellationToken ct = default);
    Task<GameInfoEntity?> LoadGameInfoAsync(CancellationToken ct = default);

}
