using CoreMap;
using GameHost.Core.Features;
using GameHost.Features.LinuxGameServer.Application.Contracts.Responses;
using GameHost.Features.LinuxGameServer.Application.Models;
using GameHost.Features.LinuxGameServer.Application.Pulses.States;
using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Features.LinuxGameServer.Domain.Entities;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Contracts.Responses;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;
using LunaticPanel.Core.Utils.Abstraction.SafeFileWriter;
using StatePulse.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    private readonly IPluginSystemLocation _pluginSystemLocation;
    private readonly IPluginUserLocation _pluginUserLocation;
    private readonly ILinuxCommand _linuxCommand;
    private readonly ICrazyReport _crazyReport;
    private readonly ICoreMap _coreMap;
    private readonly IStateAccessor<InstallationState> _installationStateAccess;
    private readonly ISafeFileWriter _safeFileWriter;
    private const string MOCK_FOLDER = "mockup";
    private const string MOCK_INSTALLER_FOLDER = "installers";

    private JsonSerializerOptions _jsonSerializerOptions = new()
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
#if DEBUG
        WriteIndented = true
#endif
    };
    public LinuxGameServerService(IPluginLocation pluginLocation,
        ILinuxCommand linuxCommand,
        ICrazyReport<LinuxGameServerService> crazyReport, ICoreMap coreMap, IStateAccessor<InstallationState> installationStateAccess,
        ISafeFileWriter safeFileWriter)
    {

        _pluginSystemLocation = pluginLocation;
        _pluginUserLocation = pluginLocation;
        _linuxCommand = linuxCommand;
        _crazyReport = crazyReport;
        _coreMap = coreMap;
        _installationStateAccess = installationStateAccess;
        _safeFileWriter = safeFileWriter;
        crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
        _pluginUserLocation.SetUsername(LinuxGameServerKeys.USERNAME);
    }

    public async Task<ICollection<GameManifestEntity>?> GetAvailableGames(CancellationToken ct = default)
    {
        // TODO: SUPPORT PRODUCTION VS DEV
        var manifestDownloadTarget = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, LinuxGameServerKeys.SERVER_MANIFEST_RESPO_FILE);
#if DEBUG
        _crazyReport.ReportWarning("Debug Detected");
        var sourceOfManifest = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, [MOCK_FOLDER], "manifest.dev.json");
        var manifestCommand = $"cp -f \"{sourceOfManifest}\" \"{manifestDownloadTarget}\"";
        var manifestReuslt = await _linuxCommand.BuildCommand(manifestCommand)
            .SetCrazyReport(_crazyReport)
            .ExecAsync(ct);
        if (manifestReuslt.Failed || !File.Exists(manifestDownloadTarget))
            throw new CouldNotDownloadManifestException(_crazyReport);
#else

#endif


        try
        {
            _crazyReport.Report("Reading Available Game Manifest...");
            string json = File.ReadAllText(manifestDownloadTarget);
            _crazyReport.Report("Deserializing Available Game Manifest...");
            var response = JsonSerializer.Deserialize<List<Contracts.Responses.GameManifestResponse>>(json, _jsonSerializerOptions);
            _crazyReport.Report("Mapping Available Game Manifest...");
            var entitesResponse = _coreMap.MapEach(response!).To<GameManifestEntity>();
            _crazyReport.ReportInfo("Available Game Server Manifest Loaded.");
            return entitesResponse;
        }
        catch (Exception ex)
        {
            throw new GameManifestFileFailedToDeserializeException(ex, _crazyReport);
        }
    }

    public async Task<GameServerInstallProcessModel?> GetInstallationProgress(CancellationToken ct = default)
    {
        string file = _pluginSystemLocation.GetConfigFor(LinuxGameServerKeys.MODULE_NAME, LinuxGameServerKeys.SERVER_INSTALL_PROGRESS_FILE);
        if (!File.Exists(file)) return default;
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<InstallationProgressStateDto>(jsonString)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameServerInstallProcessModel>();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }
    }
    public async Task<GameServerInfoEntity?> GetInstalledGameServer(CancellationToken ct = default)
    {
        string file = _pluginSystemLocation.GetConfigFor(LinuxGameServerKeys.MODULE_NAME, LinuxGameServerKeys.SERVER_INSTALL_STATE_FILE);
        if (!File.Exists(file)) return default;
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            InstallationStateDto result = JsonSerializer.Deserialize<InstallationStateDto>(jsonString)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameServerInfoEntity>();
            return entity;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportErrorException(ex.Message, ex);
            return default;
        }
    }

    public async Task PerformServerInstallation(string id, string installerName, CancellationToken ct = default)
    {
        string targetLocation = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, installerName);
        string extractionLocation = _pluginUserLocation.GetUserBashBase(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER]);
        string consoleBinaryFile = _pluginUserLocation.GetUserBashFor(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER], id);
#if DEBUG
        _crazyReport.ReportWarning("Debug Detected");
        string sourceLocation = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, [MOCK_FOLDER, MOCK_INSTALLER_FOLDER], installerName);
        var installerCopieCommand = $"cp -f \"{sourceLocation}\" \"{targetLocation}\"";
        var installerCopieCommandResult = await _linuxCommand.BuildCommand(installerCopieCommand)
            .SetCrazyReport(_crazyReport)
            .ExecAsync(ct);
        if (installerCopieCommandResult.Failed || !File.Exists(targetLocation))
            throw new CouldNotDownloadInstallerException(_crazyReport);
#else
 // TODO: IMPLEMENT PRODUCTION BEHAVIOR
#endif
        var extractCommand = $"tar -xzf \"{targetLocation}\" -C \"{extractionLocation}\"";
        var extractCommandResult = await _linuxCommand
            .BuildCommand(extractCommand)
            .AndCommand($"chmod -R 755 \"{extractionLocation}\"")
            .ExecAsync(ct);
        if (extractCommandResult.Failed && !File.Exists(consoleBinaryFile))
            throw new CouldNotExtractGameInstallerException(_crazyReport);

        var initializeCommand = $"\"{consoleBinaryFile}\" initialize";
        var initializeCommandResult = await _linuxCommand
            .BuildCommand(initializeCommand)
            .SetCrazyReport(_crazyReport)
            .ExecPayloadAsync<InstallerResponse<string>>(ct);
        if (initializeCommandResult != default && initializeCommandResult.Error != default)
            throw new CouldNotInitializeInstallerException(_crazyReport);

        var installCommand = $"\"{consoleBinaryFile}\" setup --install";
        _ = Task.Run(() => _linuxCommand.BuildCommand(installCommand)
            .SetCrazyReport(_crazyReport)
        .ExecAsync(ct));
    }

}
