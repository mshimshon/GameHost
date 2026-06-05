namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.ServerInfo;

public sealed record ExternalServerInfoConnectionResponse
{

    public string Name { get; set; } = default!;
    public string Port { get; set; } = default!;
    public string Protocol { get; set; } = default!;
}
