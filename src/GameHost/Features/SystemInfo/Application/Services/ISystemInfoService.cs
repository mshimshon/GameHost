using GameHost.Features.SystemInfo.Domain.Entites;

namespace GameHost.Features.SystemInfo.Application.Services;

public interface ISystemInfoService
{
    Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default);
}
