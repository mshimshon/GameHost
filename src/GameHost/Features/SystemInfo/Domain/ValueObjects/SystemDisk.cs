namespace GameHost.Features.SystemInfo.Domain.ValueObjects;

public sealed record SystemDisk
{
    public float Current { get; init; }
    public float Total { get; init; }
    public float Percentage { get; }
    public SystemDisk(float current, float total)
    {
        if (current > total) current = total;
        if (current < 0) current = 0;
        Percentage = total <= 0f ? 0 : MathF.Round(current / total, 2);
        Current = current;
        Total = total;
    }
}
