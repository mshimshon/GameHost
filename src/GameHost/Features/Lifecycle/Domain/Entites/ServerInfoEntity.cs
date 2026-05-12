using GameHost.Features.Lifecycle.Domain.Enums;
using GameHost.Features.Lifecycle.Domain.ValueObjects;

namespace GameHost.Features.Lifecycle.Domain.Entites;

public sealed record ServerInfoEntity
{
    public Status Status { get; init; }
    public IReadOnlyCollection<ConnectionPort>? ConnectionPorts { get; }
    public string? Address { get; }
    public DateTime LastUpdate { get; init; }
    public ServerInfoEntity(Status status)
    {
        Status = status;
    }

    public ServerInfoEntity(Status status, string address, ICollection<ConnectionPort> connectionPorts)
    {
        Status = status;
        Address = address;
        ConnectionPorts = connectionPorts.ToList().AsReadOnly();
    }
}
