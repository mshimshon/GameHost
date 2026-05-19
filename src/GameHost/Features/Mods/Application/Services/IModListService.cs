using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Application.Services;

public interface IModListService
{
    Task CreateAsync(ModListDescriptor descriptor, CancellationToken ct = default);
    Task<ModListEntity?> GetAsync(Guid id, CancellationToken ct = default);
    Task<ICollection<ModListDescriptor>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyCollection<ModSchemaPartEntity>?> GetSchematic(CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task SaveAsync(ModListEntity modListEntity, CancellationToken ct = default);
    Task SetCurrentAsync(Guid? id, CancellationToken ct = default);
    Task<Guid?> GetCurrentAsync(CancellationToken ct = default);
    Task<ModFeatureEntity?> GetModFeatureAsync(CancellationToken ct = default);



}
