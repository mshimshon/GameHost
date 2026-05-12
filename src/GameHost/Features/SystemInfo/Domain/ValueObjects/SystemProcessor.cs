namespace GameHost.Features.SystemInfo.Domain.ValueObjects;

public sealed record SystemProcessor
{
    public float Current { get; init; }
    public string Model { get; init; }
    public int Cores { get; init; }
    public SystemProcessor(float current, int cores, string model)
    {
        if (current <= 0) current = 0;
        if (current >= 100) current = 1;
        current = current / 100;
        Current = current;
        Cores = cores;
        Model = model;
    }
}
