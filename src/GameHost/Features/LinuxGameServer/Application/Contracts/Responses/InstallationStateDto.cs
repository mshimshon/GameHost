namespace GameHost.Features.LinuxGameServer.Application.Contracts.Responses;

public record InstallationStateDto
{
    public string Id { get; init; } = default!;
    public DateTime InstallDate { get; init; }
}
