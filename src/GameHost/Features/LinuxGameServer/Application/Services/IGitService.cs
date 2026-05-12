namespace GameHost.Features.LinuxGameServer.Application.Services;

public interface IGitService
{
    Task CloneAsync(string gitUrl, string target, CancellationToken ct = default);
}
