namespace GameHost.Features.SystemInfo.Infrastructure.Configurations;

public record LinuxSystemInfoConfiguration
{
    public string WorkingDisk { get; init; } = "$HOME";
    public int PeriodicResourceCheckDelaySeconds { get; init; } = 8;
}
