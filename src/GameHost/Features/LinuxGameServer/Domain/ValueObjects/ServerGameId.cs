namespace GameHost.Features.LinuxGameServer.Domain.ValueObjects;

public sealed record ServerGameId
{
    public string Value { get; }
    public ServerGameId(string value)
    {
        Value = value;
    }
}
