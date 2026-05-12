namespace GameHost.Features.Lifecycle.Domain.ValueObjects;

public sealed record ConnectionPort
{
    public ConnectionPort(string name, string port, string protocol)
    {
        Name = name;
        Port = port;
        Protocol = protocol;
    }

    public string Name { get; }
    public string Port { get; }
    public string Protocol { get; }
}
