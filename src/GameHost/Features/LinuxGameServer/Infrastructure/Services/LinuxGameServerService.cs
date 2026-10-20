using GameHost.Features.LinuxGameServer.Application.Services;
using GameHost.Features.LinuxGameServer.Domain.Entities;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Exceptions;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses;
using GameHost.Features.LinuxGameServer.Infrastructure.Services.Payloads.Responses.Mapping;
using GameHost.Keys.Features;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;
using LunaticPanel.Core.Utils.Abstraction.SafeFileWriter;
using Octokit;
using StatePulse.Net;
using System.Formats.Tar;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GameHost.Features.LinuxGameServer.Infrastructure.Services;

internal class LinuxGameServerService : ILinuxGameServerService
{
    private readonly IPluginSystemLocation _pluginSystemLocation;
    private readonly IPluginUserLocation _pluginUserLocation;
    private readonly ILinuxCommand _linuxCommand;
    private readonly ICrazyReport _crazyReport;
    private readonly IStateAccessor<Application.Pulses.States.InstallationState> _installationStateAccess;
    private readonly ISafeFileWriter _safeFileWriter;
    private const string MOCK_FOLDER = "mockup";
    private const string MOCK_INSTALLER_FOLDER = "installers";
    private readonly HttpClient _httpClient;
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
        ICrazyReport<LinuxGameServerService> crazyReport, IStateAccessor<Application.Pulses.States.InstallationState> installationStateAccess,
        ISafeFileWriter safeFileWriter)
    {

        _pluginSystemLocation = pluginLocation;
        _pluginUserLocation = pluginLocation;
        _linuxCommand = linuxCommand;
        _crazyReport = crazyReport;
        _installationStateAccess = installationStateAccess;
        _safeFileWriter = safeFileWriter;
        crazyReport.SetModule(LinuxGameServerKeys.MODULE_NAME);
        _pluginUserLocation.SetUsername(LinuxGameServerKeys.USERNAME);
        _httpClient = new HttpClient();

    }
    private async Task<string?> GetManifestFromArchiveAsync(string archive, string file)
    {
        try
        {
            // Open file, decompress GZip, and read Tar sequentially in memory
            await using FileStream fs = File.OpenRead(archive);
            await using GZipStream gzipStream = new GZipStream(fs, CompressionMode.Decompress);
            using TarReader reader = new TarReader(gzipStream);
            while (reader.GetNextEntry() is TarEntry entry)
            {
                if (entry.Name == file &&
                    entry.EntryType is TarEntryType.RegularFile or TarEntryType.V7RegularFile)
                {
                    if (entry.DataStream == null) return null;

                    using StreamReader streamReader = new StreamReader(entry.DataStream);
                    // This is where the heavy I/O actually happens, so async fits perfectly here
                    return await streamReader.ReadToEndAsync();
                }
            }

        }
        catch (Exception ex)
        {

            _crazyReport.ReportErrorException(ex.Message, ex);

        }
        return null; // File not found inside the archive
    }

    private async Task<IEnumerable<GithubReleaseVersionResponse>> GetReleasesVersions()
    {
        IReadOnlyList<Release>? releases = default;
        var client = new GitHubClient(new ProductHeaderValue("GameHost.Games"));
        var clientResult = client.Repository.Release.GetAll("mshimshon", "GameHost.Games");
        releases = await clientResult;
        _crazyReport.Report($"Github Release Status {clientResult.Status}");
        _crazyReport.Report($"Latest Release Result ? {releases.Count}");
        return releases
            .Where(r => !string.IsNullOrWhiteSpace(r.TagName))
            .Select(r =>
            {
                var match = Regex.Match(r.TagName, _regexGithubManifestTag, RegexOptions.IgnoreCase);
                if (!match.Success)
                    return null;
                string version = match.Groups[0].Value;
                return new GithubReleaseVersionResponse()
                {
                    Release = r,
                    Version = Version.Parse(version)
                };
            })
            .Where(x => x != null)
            .OrderByDescending(x => x!.Version)!;
    }

    private string _regexGithubManifestTag = @"^.+-v(\d+\.\d+\.\d+)$";
    public async Task<ICollection<GameManifestEntity>?> GetAvailableGames(CancellationToken ct = default)
    {
        // TODO: SUPPORT PRODUCTION VS DEV
        var manifestDownloadTarget = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, LinuxGameServerKeys.SERVER_MANIFEST_RESPO_FILE);
        _crazyReport.ReportWarning("===Debug Detected=== Generating Manifest.json");
        var sourceOfManifest = _pluginUserLocation.GetUserDownloadBase(LinuxGameServerKeys.MODULE_NAME, [MOCK_FOLDER, MOCK_INSTALLER_FOLDER]);
        List<GameManifestResponse> localManifests = new List<GameManifestResponse>();
        var installers = Directory.GetFiles(sourceOfManifest, "*.tar.gz", SearchOption.TopDirectoryOnly);
        _crazyReport.Report($"{installers.Length} Game Installer Found in Debug.");

        foreach (var file in installers)
        {
            string? json = await GetManifestFromArchiveAsync(file, "config/manifest.json");
            if (json == default)
            {
                _crazyReport.ReportWarning($"'{file}' Manifest Not Found (config/manifest.json).");
                continue;
            }
            GameManifestResponse? manifest = JsonSerializer.Deserialize<GameManifestResponse>(json, _jsonSerializerOptions)!;
            if (manifest == default)
            {
                _crazyReport.ReportWarning($"'{file}' Manifest Invalid.");

                continue;
            }
            localManifests.Add(manifest);
        }
        // _safeFileWriter

        var releases = await GetReleasesVersions();
        var latestRelease = releases.FirstOrDefault(p => p.Release.TagName.StartsWith("registry-v"));
        // TODO: Throw if latestRelease = null;
        if (latestRelease == default)
        {
            _crazyReport.ReportError("Github Latest Release registry-v not found.");
            throw new CouldNotDownloadManifestException(_crazyReport);
        }
        var manifestListAsset = latestRelease.Release.Assets.FirstOrDefault(a => string.Equals(a.Name, "manifests.json", StringComparison.OrdinalIgnoreCase));
        if (manifestListAsset == default)
        {
            _crazyReport.ReportError("Github Latest Release registry-v manifest list not found.");
            throw new CouldNotDownloadManifestException(_crazyReport);
        }

        await using Stream remote = await _httpClient.GetStreamAsync(manifestListAsset.BrowserDownloadUrl);
        var remoteManifest = await JsonSerializer.DeserializeAsync<List<GameManifestResponse>>(remote);
        var resultManifest = localManifests;
        resultManifest.AddRange(resultManifest.Where(p => !localManifests.Any(x => x.Id == p.Id)));
        await _safeFileWriter.WriteThenCopyFileAsync(manifestDownloadTarget, JsonSerializer.Serialize(resultManifest, _jsonSerializerOptions));
        //if (asset == null)

        // Create Multiple Release Tags
        // ConsoleName-v1.1.1 Each Build that specific project
        // Fully Self Contained Dotnet Release
        // Another Tag is required -> query-v1.1.1 which will compile all manifest.json of each projects into manifests.json and each dep_debian_X_common.json
        // Once Release is complete this code will retrive all Manifests and dependencies for the given OS.


        try
        {
            _crazyReport.Report("Reading Available Game Manifest...");
            string json = File.ReadAllText(manifestDownloadTarget);
            _crazyReport.Report("Deserializing Available Game Manifest...");
            List<GameManifestResponse> response = JsonSerializer.Deserialize<List<GameManifestResponse>>(json, _jsonSerializerOptions)!;
            _crazyReport.Report("Mapping Available Game Manifest...");
            var entitesResponse = response!.Select(p => p.MapToDomain()).ToList();
            _crazyReport.ReportInfo("{0} Available Game Server Manifest Loaded.", response.Count);
            return entitesResponse;
        }
        catch (Exception ex)
        {
            throw new GameManifestFileFailedToDeserializeException(ex, _crazyReport);
        }
    }

    public async Task<GameServerInstallProgressEntity?> GetInstallationProgress(CancellationToken ct = default)
    {
        string file = _pluginSystemLocation.GetConfigFor(LinuxGameServerKeys.MODULE_NAME, LinuxGameServerKeys.SERVER_INSTALL_PROGRESS_FILE);
        if (!File.Exists(file)) return default;
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<GameServerInstallProgressResponse>(jsonString)!;
            if (result == default) return default;
            var entity = result.MapToDomain();
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
            await CopyInstallerAssetsToDynamicRoot(ct);
            string jsonString = await File.ReadAllTextAsync(file);
            GameServerInfoResponse result = JsonSerializer.Deserialize<GameServerInfoResponse>(jsonString)!;
            if (result == default) return default;
            var entity = result.MapToDomain();
            return entity;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportErrorException(ex.Message, ex);
            return default;
        }
    }

    private async Task CopyInstallerAssetsToDynamicRoot(CancellationToken ct = default)
    {
        string baseDynamicPath = _pluginSystemLocation.GetDynamicWebContentBase(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER]);
        string[] serverControlAssetSub = [LinuxGameServerKeys.SERVER_CONTROL_FOLDER, LinuxGameServerKeys.SERVER_CONTROL_ASSET_FOLDER];
        string serverControlAssetsLocation = _pluginUserLocation.GetUserBashBase(LinuxGameServerKeys.MODULE_NAME, serverControlAssetSub);
        // rm -rf /etc/aa/wwwroot && mkdir -p /etc/aa/wwwroot && cp -a /etc/xx/assets/. /etc/aa/wwwroot/
        if (Path.Exists(baseDynamicPath))
        {
            Directory.Delete(baseDynamicPath, true);
            Directory.CreateDirectory(baseDynamicPath);
        }

        var assetCopyCommand = $"cp -a \"{serverControlAssetsLocation}/.\" \"{baseDynamicPath}/\"";
        var installerCopieCommandResult = await _linuxCommand.BuildCommand(assetCopyCommand)
            .AndCommand($"chmod -R 755 \"{baseDynamicPath}\"")
            .SetCrazyReport(_crazyReport)
            .ExecAsync(ct);
        if (installerCopieCommandResult.Failed)
            throw new FailedToCopyAssetsToServeLocationException(_crazyReport);

    }

    public async Task PerformServerInstallation(string id, string installerName, CancellationToken ct = default)
    {
        string targetLocation = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, installerName);
        string targetLocationDownload = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, $"{installerName}.download");
        string extractionLocation = _pluginUserLocation.GetUserBashBase(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER]);
        string consoleBinaryFile = _pluginUserLocation.GetUserBashFor(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER], id);

        string sourceLocation = _pluginUserLocation.GetUserDownloadFor(LinuxGameServerKeys.MODULE_NAME, [MOCK_FOLDER, MOCK_INSTALLER_FOLDER], installerName);
        if (File.Exists(sourceLocation))
        {
            _crazyReport.Report($"Installing from '{sourceLocation}'");
            var installerCopieCommand = $"cp -f \"{sourceLocation}\" \"{targetLocation}\"";
            var installerCopieCommandResult = await _linuxCommand.BuildCommand(installerCopieCommand)
                .SetCrazyReport(_crazyReport)
                .ExecAsync(ct);
            if (installerCopieCommandResult.Failed)
                throw new CouldNotDownloadInstallerException(_crazyReport);
        }
        else
        {
            var releases = await GetReleasesVersions();
            var latestRelease = releases.FirstOrDefault(p => p.Release.TagName.StartsWith($"{installerName}-v", StringComparison.OrdinalIgnoreCase));
            // TODO: Throw if latestRelease = null;
            if (latestRelease == default)
            {
                _crazyReport.ReportError($"Github Latest Release {installerName}-v not found.");
                throw new CouldNotDownloadInstallerException(_crazyReport);
            }
            var manifestListAsset = latestRelease.Release.Assets
                .Where(p => p.Name.StartsWith(installerName, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault(p => p.Name.EndsWith("tar.gz", StringComparison.OrdinalIgnoreCase));
            if (manifestListAsset == default)
            {
                _crazyReport.ReportError($"Github Latest Release {installerName}-v manifest list not found.");
                throw new CouldNotDownloadInstallerException(_crazyReport);
            }
            _crazyReport.ReportInfo($"Downloading Installer from {manifestListAsset.BrowserDownloadUrl}.");
            await using Stream remote = await _httpClient.GetStreamAsync(manifestListAsset.BrowserDownloadUrl);
            await using FileStream file = File.Create(targetLocationDownload);
            await remote.CopyToAsync(file);
            File.Move(targetLocationDownload, targetLocation, true);
        }
        if (!File.Exists(targetLocation))
            throw new CouldNotDownloadInstallerException(_crazyReport);
        var extractCommand = $"tar -xzf \"{targetLocation}\" -C \"{extractionLocation}\"";
        var extractCommandResult = await _linuxCommand
            .BuildCommand(extractCommand)
            .AndCommand($"chmod -R 755 \"{extractionLocation}\"")
            .ExecAsync(ct);
        if (extractCommandResult.Failed && !File.Exists(consoleBinaryFile))
            throw new CouldNotExtractGameInstallerException(_crazyReport);
        await CopyInstallerAssetsToDynamicRoot(ct);
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
