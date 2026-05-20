namespace GameHost.Features.SystemInfo.Infrastructure.Configurations;

public record LinuxSystemInfoConfiguration
{
    public string WorkingDisk { get; init; } = "/home";
    public int PeriodicResourceCheckDelaySeconds { get; init; } = 8;
}
