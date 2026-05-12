using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Git.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;
namespace GameHost.Features.LinuxGameServer.Infrastructure.Services.Git;

internal class GitService : IGitService
{
    private readonly IPluginSystemLocation _pluginSystemLocation;
    private readonly ILinuxCommand _linuxCommand;
    private readonly ICrazyReport<GitService> _crazyReport;

    public GitService(IPluginLocation pluginLocation, ILinuxCommand linuxCommand, ICrazyReport<GitService> crazyReport)
    {
        _pluginSystemLocation = pluginLocation;
        _linuxCommand = linuxCommand;
        _crazyReport = crazyReport;
    }

    public async Task CloneAsync(string gitUrl, string target, CancellationToken ct = default)
    {
        var isGitInstalled = await _linuxCommand
            .BuildCommand("command -v git >/dev/null 2>&1")
            .AndPrintPayload(bool.TrueString)
            .OrPrintPayload(bool.FalseString)
            .SetCrazyReport(_crazyReport)
            .ExecPayloadAsync<bool>(ct);
        if (!isGitInstalled)
            throw new GitPackageNotInstalledException();

        var targetFolder = _pluginSystemLocation.GetReposFor(LinuxGameServerKeys.MODULE_NAME, target);
        // [[ -n "${TARGET_DIR:-}" && -d "$TARGET_DIR"]] && echo true || echo false
        var isTargetValid = await _linuxCommand
            .BuildCommand($"-n \"{targetFolder}\" && -d \"{targetFolder}\"", "[[{0}]]")
            .AndPrintPayload(bool.TrueString)
            .OrPrintPayload(bool.FalseString)
            .SetCrazyReport(_crazyReport)
            .ExecPayloadAsync<bool>(ct);
        if (isTargetValid)
        {
            Directory.Delete(targetFolder);
            targetFolder = _pluginSystemLocation.GetReposFor(LinuxGameServerKeys.MODULE_NAME, target);
        }
        var cloneResult = await _linuxCommand
                .BuildCommand($"git clone --verbose \"{gitUrl}\" \"{targetFolder}\"")
            //.ThrowOnFailure<Exception>()
            .SetCrazyReport(_crazyReport)
                .ExecAsync(ct);
        if (cloneResult.Failed)
            throw new GitCloneFailedException();

        // Fix Permissions
        var permissionFixResult = await _linuxCommand.BuildCommand($"chmod 775 -R {targetFolder}").ExecAsync(ct);
        if (permissionFixResult.Failed)
            throw new GitPermissionFixFailedException();

    }

}
