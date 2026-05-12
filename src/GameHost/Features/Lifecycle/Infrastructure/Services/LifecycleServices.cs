using CoreMap;
using GameHost.Core.Features;
using GameHost.Features.Lifecycle.Application.Services;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Infrastructure.Services.Contracts;
using GameHost.Features.Lifecycle.Infrastructure.Services.Contracts.Status;
using GameHost.Features.Lifecycle.Infrastructure.Services.Exceptions;
using GameHost.Kernel.Abstractions.Dto;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Extensions;
using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;
using LunaticPanel.Core.Utils.Abstraction.SafeFileWriter;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace GameHost.Features.Lifecycle.Infrastructure.Services;

internal class LifecycleServices : ILifecycleServices, IGameInfoService, IStartupParameterService
{
    private readonly IQueryBus _queryBus;
    private readonly ILinuxCommand _linuxCommand;
    private readonly ICoreMap _coreMap;
    private readonly ISafeFileWriter _safeFileWriter;
    private readonly IPluginUserLocation _pluginUserLocation;
    private readonly IPluginSystemLocation _pluginSystemLocation;
    private readonly ICrazyReport _crazyReport;
    private string? _gameId;

    private readonly JsonSerializerOptions _jsonSerializerConfiguration = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
#if DEBUG
        WriteIndented = true,
#endif
    };
    private string? _rawGameInfo;
    public LifecycleServices(IQueryBus queryBus,
        ILinuxCommand linuxCommand,
        ICoreMap coreMap,
        ISafeFileWriter safeFileWriter,
        IPluginLocation pluginLocation,
        ICrazyReport<LifecycleServices> crazyReport)
    {
        _queryBus = queryBus;
        _linuxCommand = linuxCommand;
        _coreMap = coreMap;
        _safeFileWriter = safeFileWriter;
        _pluginUserLocation = pluginLocation;
        _pluginSystemLocation = pluginLocation;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(LifecycleKeys.MODULE_NAME);
        _pluginUserLocation.SetUsername(LinuxGameServerKeys.USERNAME);

    }
    public async Task<Dictionary<string, string>> GetServerStartupParametersAsync(CancellationToken cancellationToken = default)
    {
        string file = _pluginUserLocation.GetUserConfigFor(LifecycleKeys.MODULE_NAME, LifecycleKeys.USER_STARTUP_PARAM_FILE);
        _crazyReport.ReportInfo("Checking({1}) {0} ", file, File.Exists(file));
        if (!File.Exists(file)) return new();
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<List<GameStartupParameterKeyValueResponse>>(jsonString, _jsonSerializerConfiguration)!;
            if (result == default) return new();
            return result.ToDictionary(p => p.Key, p => p.Value); ;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unable to load {LifecycleKeys.USER_STARTUP_PARAM_FILE}", ex);
        }
    }

    public async Task<ICollection<GameStartupParameterHostDefResponse>> GetHostServerStartupParametersAsync(CancellationToken cancellationToken = default)
    {
        string file = _pluginSystemLocation.GetConfigFor(LifecycleKeys.MODULE_NAME, LifecycleKeys.HOST_STARTUP_PARAM_FILE);
        _crazyReport.ReportInfo("Checking({1}) {0} ", file, File.Exists(file));
        if (!File.Exists(file)) return new List<GameStartupParameterHostDefResponse>();
        try
        {
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.ReportInfo(jsonString);
            var result = JsonSerializer.Deserialize<List<GameStartupParameterHostDefResponse>>(jsonString, _jsonSerializerConfiguration)!;
            if (result == default) return new List<GameStartupParameterHostDefResponse>();
            return result;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unable to load {LifecycleKeys.HOST_STARTUP_PARAM_FILE}", ex);
        }
    }

    public async Task<string?> GetRawGameInfoAsync(CancellationToken ct = default)
    {
        try
        {
            if (_rawGameInfo != default)
                return _rawGameInfo;

            string file = _pluginUserLocation
                .GetUserBashFor(LinuxGameServerKeys.MODULE_NAME, [
                    LinuxGameServerKeys.SERVER_CONTROL_FOLDER,
                    LinuxGameServerKeys.SERVER_CONTROL_CONFIG_FOLDER], LifecycleKeys.GAME_INFO_FILE);

            _crazyReport.ReportInfo("Checking({1}) {0} ", file, File.Exists(file));
            if (!File.Exists(file)) return default;
            string jsonString = await File.ReadAllTextAsync(file);
            _crazyReport.Report(jsonString);
            _rawGameInfo = jsonString;
            return _rawGameInfo;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unable to load {LifecycleKeys.GAME_INFO_FILE}", ex);
        }
    }

    public async Task<GameInfoEntity?> LoadGameInfoAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var jsonString = await GetRawGameInfoAsync(cancellationToken);
            if (jsonString == default) return default;
            var result = JsonSerializer.Deserialize<GameInfoResponse>(jsonString, _jsonSerializerConfiguration)!;
            if (result == default) return default;
            var entity = _coreMap.Map(result).To<GameInfoEntity>();
            return entity;
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError(ex.Message);

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unable to load {LifecycleKeys.GAME_INFO_FILE}", ex);
        }
    }

    public async Task ServerRestartAsync(CancellationToken ct = default)
        => await ServerConsoleCommandExec("--restart", () => new FailedToExecuteStartCommandException(_crazyReport), ct);

    private async Task ServerConsoleCommandExec(string command, Func<WebServiceException> onFailure, CancellationToken ct = default)
    {
        var gameId = await GetGameIdAsync();
        var binaryConsoleFile = _pluginUserLocation.GetUserBashFor(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER], gameId);

        var commandResult = await _linuxCommand.BuildCommand($"{binaryConsoleFile} server {command}")
            .SetCrazyReport(_crazyReport)
            .ExecAsync(ct);
        if (commandResult.Failed)
            throw onFailure();
    }
    public async Task ServerStartAsync(CancellationToken ct = default)
        => await ServerConsoleCommandExec("--start", () => new FailedToExecuteStartCommandException(_crazyReport), ct);
    private async Task<string> GetGameIdAsync()
    {
        if (_gameId != default) return _gameId;
        var gameIdResponse = await _queryBus.QueryWithoutDataAsync(LinuxGameServerKeys.Queries.GET_GAME_ID);
        var result = await gameIdResponse.ReadAs<string>();
        if (result == default)
            throw new GameServerNotInstalledException(_crazyReport);
        _gameId = result;
        return result;
    }
    public async Task<ServerInfoEntity?> ServerStatusAsync(CancellationToken ct = default)
    {
        var gameId = await GetGameIdAsync();
        var binaryConsoleFile = _pluginUserLocation.GetUserBashFor(LinuxGameServerKeys.MODULE_NAME,
            [LinuxGameServerKeys.SERVER_CONTROL_FOLDER], gameId);

        var commandResult = await _linuxCommand
            .BuildCommand($"{binaryConsoleFile} server --status")
            .SetCrazyReport(_crazyReport)
            .ExecAsync(ct);

        if (commandResult.Failed)
            throw new FailedToGetServerStatusException(_crazyReport);
        var result = commandResult.PayloadAsOrDefault<InstallerResultDataDto<StatusResponse>>(default);
        if (result?.Data == default)
            throw new FailedToGetServerStatusException(_crazyReport);
        var resultEntity = _coreMap.Map(result.Data).To<ServerInfoEntity>();
        return resultEntity;
    }

    public async Task ServerStopAsync(CancellationToken ct = default)
        => await ServerConsoleCommandExec("--stop", () => new FailedToExecuteStartCommandException(_crazyReport), ct);

    public async Task UpdateStartupParameterAsync(string key, string value, CancellationToken ct = default)
    {
        var lockFile = _pluginUserLocation.GetUserConfigFor(LifecycleKeys.MODULE_NAME, LifecycleKeys.USER_STARTUP_PARAM_LOCKFILE);
        var userStartUpParamFile = _pluginUserLocation.GetUserConfigFor(LifecycleKeys.MODULE_NAME, LifecycleKeys.USER_STARTUP_PARAM_FILE);
        FileStream? stream = default;
        try
        {
            stream = File.Open(lockFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
            var current = await GetServerStartupParametersAsync(ct);
            current[key] = value;
            var toWriteList = current.Select(p => new GameStartupParameterKeyValueResponse()
            {
                Key = p.Key,
                Value = p.Value
            });
            var json = JsonSerializer.Serialize(toWriteList, _jsonSerializerConfiguration);
            await _safeFileWriter.WriteThenCopyFileAsync(userStartUpParamFile, json, ct);
        }
        catch (Exception ex)
        {
            throw new StartupParameterFailedToAcquireLockException(ex, _crazyReport);
        }
        finally
        {
            if (stream != default)
                try
                {
                    stream.Close();

                    File.Delete(lockFile);
                }
                catch { }
        }
    }
}
