using GameHost.Features.SystemInfo.Domain.ValueObjects;

namespace GameHost.Features.SystemInfo.Domain.Entites;

public sealed record SystemInfoEntity
{
    public SystemMemory Memory { get; init; } = default!;
    public SystemDisk Disk { get; init; } = default!;
    public SystemProcessor Processor { get; init; } = default!;
}
