using MedihatR;

namespace GameHost.Features.LinuxGameServer.Application.Mediator.Commands;

public record InstallGameServerCommand(string Id, string InstallerName) : IRequest
{
}
