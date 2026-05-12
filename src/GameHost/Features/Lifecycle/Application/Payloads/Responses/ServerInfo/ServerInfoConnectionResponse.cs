namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.ServerInfo;

public sealed record ServerInfoConnectionResponse
{

    public string Name { get; set; } = default!;
    public string Port { get; set; } = default!;
    public string Protocol { get; set; } = default!;
}
