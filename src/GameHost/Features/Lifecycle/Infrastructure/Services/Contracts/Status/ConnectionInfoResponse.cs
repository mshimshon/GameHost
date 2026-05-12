namespace GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status;

public record ConnectionInfoResponse
{
    public string Address { get; set; } = default!;
    public List<PortInfoResponse> PortInfoResponses { get; set; } = default!;
}
