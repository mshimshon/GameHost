using GameHost.Core.Features;
using GameHost.Features.Mods.Application.Services;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;
using GameHost.Features.Mods.Infrastructure.Exceptions;
using GameHost.Features.Mods.Infrastructure.Services.ModList.Exceptions;
using GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads;
using GameHost.Features.Mods.Infrastructure.Services.ModList.Payloads.Mapping;
using GameHost.Kernel.Abstractions.Exceptions;
using GameHost.Kernel.Abstractions.Mediator.Exceptions;
using LunaticPanel.Core.Abstraction.Messaging.QuerySystem;
using LunaticPanel.Core.Extensions;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;
using LunaticPanel.Core.Utils.Abstraction.SafeFileWriter;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GameHost.Features.Mods.Infrastructure.Services.ModList;

internal sealed class ModListService : IModListService
{
    private readonly IPluginUserLocation _pluginUserLocation;
    private readonly ISafeFileWriter _safeFileWriter;
    private readonly ILinuxCommand _linuxCommand;
    private readonly IQueryBus _queryBus;
    private readonly ICrazyReport<ModListService> _crazyReport;
    private readonly JsonSerializerOptions _serializerOption = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public ModListService(IPluginLocation pluginLocation, ISafeFileWriter safeFileWriter,
        ILinuxCommand linuxCommand, IQueryBus queryBus, ICrazyReport<ModListService> crazyReport)
    {
        _pluginUserLocation = pluginLocation;
        _safeFileWriter = safeFileWriter;
        _linuxCommand = linuxCommand;
        _queryBus = queryBus;
        _crazyReport = crazyReport;
        _pluginUserLocation.SetUsername(LinuxGameServerKeys.USERNAME);
    }
    public async Task CreateAsync(ModListDescriptor descriptor, CancellationToken ct = default)
    {
        if (descriptor.Id == Guid.Empty)
            throw new WebServiceException("", $"{descriptor.Id} is not defined."); // TODO: Localize
        //TODO: DEFINE EXCEPTION


        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, [ModListKeys.USER_SAVED_MODLIST_FOLDER_NAME], $"modlist-{descriptor.Id}.json");
        bool fileDoesExist = File.Exists(filename);
        if (fileDoesExist)
            throw new WebServiceException("", $"ModList {descriptor.Id} already exist."); // TODO: Localize
        //TODO: DEFINE EXCEPTION

        try
        {
            var dto = new ModListResponse()
            {
                Id = descriptor.Id,
                Name = descriptor.Name
            };
            var content = JsonSerializer.Serialize(dto, _serializerOption);
            await File.WriteAllTextAsync(filename, content, ct);
        }
        catch (OperationCanceledException ex)
        {


            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Operation to create modlist {descriptor.Id} was cancelled.", ex);
        }
        catch
        {
            throw;
        }


    }

    // TODO: Switch Pagination Model with Lightweight DTO usage instead of JsonNode.
    public async Task<ICollection<ModListDescriptor>> GetAllAsync(CancellationToken ct = default)
    {
        string path = _pluginUserLocation.GetUserConfigBase(ModListKeys.MODULE_NAME, [ModListKeys.USER_SAVED_MODLIST_FOLDER_NAME]);
        var modlists = Directory.EnumerateFiles(path, "modlist-*.json");
        List<ModListDescriptor> result = new();
        foreach (var item in modlists)
        {
            try
            {
                var content = await File.ReadAllTextAsync(item, ct);
                var node = JsonNode.Parse(content);
                string? name = node?[nameof(ModListDescriptor.Name)]?.GetValue<string>();
                Guid? id = node?[nameof(ModListDescriptor.Id)]?.GetValue<Guid>();
                if (id == default || name == default)
                    continue;
                result.Add(new((Guid)id!, name));
            }
            catch
            {
                continue;
            }
        }
        return result;
    }

    public async Task<ModListEntity?> GetAsync(Guid id, CancellationToken ct = default)
    {
        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, [ModListKeys.USER_SAVED_MODLIST_FOLDER_NAME], $"modlist-{id}.json");
        _crazyReport.ReportInfo("Trying to load ModList {0}.", filename);
        bool fileDoesNotExist = !File.Exists(filename);
        if (fileDoesNotExist)
            throw new WebServiceException("", $"ModList {id} does not exist."); // TODO: Localize
        //TODO: DEFINE EXCEPTION
        try
        {
            string content = await File.ReadAllTextAsync(filename, ct);
            var dto = JsonSerializer.Deserialize<ModListResponse>(content, _serializerOption);
            if (dto == default)
                throw new WebServiceException("", $"The ModList {id} seems to be corrupted.", _crazyReport); // TODO: Localize
            _crazyReport.ReportInfo("Loaded ModList {0}.", dto.Name);
            //TODO: DEFINE EXCEPTION
            var result = dto.MapToDomain();
            _crazyReport.Report("Converted Modlist from Contract to Entity {0}.", result.Descriptor.Name);
            return result;
        }
        catch (OperationCanceledException ex)
        {
            string errorMessage = $"Operation to get modlist {id} was cancelled."; // TODO: Localize
            throw new WebServiceException("", errorMessage, ex, _crazyReport);
            //TODO: DEFINE EXCEPTION
        }
        catch (JsonException ex)
        {
            string errorMessage = $"The ModList {id} seems to be corrupted."; // TODO: Localize
            throw new WebServiceException("", errorMessage, ex, _crazyReport);
            //TODO: DEFINE EXCEPTION
        }
        catch (Exception ex)
        {
            string errorMessage = $"Unknown error has occured"; // TODO: Localize
            throw new WebServiceException("", errorMessage, ex, _crazyReport);
            //TODO: DEFINE EXCEPTION
        }
    }
    public async Task SaveAsync(ModListEntity modListEntity, CancellationToken ct = default)
    {
        string id = modListEntity.Descriptor.Id.ToString();
        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, [ModListKeys.USER_SAVED_MODLIST_FOLDER_NAME], $"modlist-{id}.json");
        string tmp = Path.GetTempFileName();
        bool fileDoesNotExist = !File.Exists(filename);
        if (fileDoesNotExist)
            throw new WebServiceException("", $"ModList {id} does not exist."); // TODO: Localize
        //TODO: DEFINE EXCEPTION
        try
        {

            var toCommit = modListEntity.MapToInfrastructure();
            var json = JsonSerializer.Serialize(toCommit, _serializerOption);
            await File.WriteAllTextAsync(tmp, json, ct);
            await _safeFileWriter.WriteThenCopyFileAsync(filename, json, ct);
        }
        catch (OperationCanceledException ex)
        {
            try { File.Delete(tmp); } catch { }
            throw new WebServiceException("", $"Operation to save modlist {id} was cancelled.", ex, _crazyReport); // TODO: Localize
            //TODO: DEFINE EXCEPTION
        }
        catch (JsonException ex)
        {
            try { File.Delete(tmp); } catch { }
            throw new WebServiceException("", $"The ModList {id} seems to be corrupted.", ex, _crazyReport); // TODO: Localize
            //TODO: DEFINE EXCEPTION
        }
        catch (Exception ex)
        {
            try { File.Delete(tmp); } catch { }
            throw new WebServiceException("", $"Unknown Error", ex, _crazyReport); // TODO: Localize
            //TODO: DEFINE EXCEPTION
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, [ModListKeys.USER_SAVED_MODLIST_FOLDER_NAME], $"modlist-{id}.json");
        bool fileDoesNotExist = !File.Exists(filename);
        if (fileDoesNotExist) return;
        try
        {
            File.Delete(filename);
        }
        catch (OperationCanceledException ex)
        {
            throw new WebServiceException("", $"Operation to delete modlist {id} was cancelled.", ex, _crazyReport); // TODO: Localize
            //TODO: DEFINE EXCEPTION
        }
        catch (Exception ex)
        {
            throw new WebServiceException("", $"Unknown Error", ex, _crazyReport); // TODO: Localize
            //TODO: DEFINE EXCEPTION
        }
    }

    public async Task<IReadOnlyCollection<ModSchemaPartEntity>?> GetSchematic(CancellationToken ct = default)
    {
        try
        {
            var qryResult = await _queryBus.QueryWithoutDataAsync(LifecycleKeys.Queries.GET_RAW_GAME_INFO);
            var json = await qryResult.ReadAs<string>();
            if (json == default) return default;
            var gameinfo = JsonSerializer.Deserialize<ModSchemaResponse>(json, _serializerOption);
            if (gameinfo == default) return default;
            if (gameinfo.ModSchema == default) return default;
            return gameinfo.MapToDomain().Parts;
        }
        catch (JsonException ex)
        {
            throw new WebServiceException("", $"The ModList schematic seems to be corrupted.", ex, _crazyReport); // TODO: Localize
            //TODO: DEFINE EXCEPTION
        }
        catch (Exception ex)
        {
            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unknown Error", ex, _crazyReport); // TODO: Localize
        }

    }


    private async Task RemoveCurrentAsync(CancellationToken ct = default)
    {
        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, $"modlist_current.json");
        bool fileExist = File.Exists(filename);
        if (!fileExist) return;
        try
        {
            File.Delete(filename);
        }
        catch (UnauthorizedAccessException ex)
        {//TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Operation permission denied.", ex, _crazyReport); // TODO: Localize
        }
        catch (PathTooLongException ex)
        {

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"The path is too long.", ex, _crazyReport); // TODO: Localize
        }
        catch (DirectoryNotFoundException ex)
        {

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"The directory was not found.", ex, _crazyReport); // TODO: Localize
        }
        catch (Exception ex)
        {

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unknown Error.", ex, _crazyReport); // TODO: Localize
        }
    }
    public async Task SetCurrentAsync(Guid? id, CancellationToken ct = default)
    {
        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, $"modlist_current.json");
        bool fileExist = File.Exists(filename);
        if (id == default || id == Guid.Empty)
        {
            await RemoveCurrentAsync(ct);
            return;
        }
        try
        {
            await _safeFileWriter.WriteThenCopyFileAsync(filename, id.ToString()!, ct);
        }
        catch (OperationCanceledException)
        {
            throw new SetCurrentModlistCancelledException();
        }
        catch (JsonException ex)
        {
            throw new CouldNotSerializeIdForCurrentModlistException(ex, _crazyReport);
        }
        catch (Exception ex)
        {
            throw new UnknownWebServiceException(ex, _crazyReport);
        }
    }

    public async Task<Guid?> GetCurrentAsync(CancellationToken ct = default)
    {
        string filename = _pluginUserLocation.GetUserConfigFor(ModListKeys.MODULE_NAME, $"modlist_current.json");
        bool fileDoesNotExist = !File.Exists(filename);
        if (fileDoesNotExist)
            return default;
        try
        {
            string strGuid = await File.ReadAllTextAsync(filename, ct);
            bool idInvalid = !Guid.TryParse(strGuid, out Guid result);
            if (idInvalid)
            {
                File.Delete(filename);
                return default;
            }
            return result;
        }
        catch (Exception ex)
        {

            //TODO: DEFINE EXCEPTION
            throw new WebServiceException("", $"Unknown Error.", ex, _crazyReport); // TODO: Localize
        }

    }

    public async Task<ModFeatureEntity?> GetModFeatureAsync(CancellationToken ct = default)
    {
        try
        {
            var gameId = await _queryBus.QueryWithoutDataAsync<string>(LinuxGameServerKeys.Queries.GET_GAME_ID);
            if (gameId == default)
            {
                _crazyReport.ReportError($"{nameof(LinuxGameServerKeys.Queries.GET_GAME_ID)} is not defined.");
                return default;
            }
            var binaryConsoleFile = _pluginUserLocation.GetUserBashFor(LinuxGameServerKeys.MODULE_NAME, [LinuxGameServerKeys.SERVER_CONTROL_FOLDER], gameId);
            var modFeatureDetails = await _linuxCommand.BuildCommand($"{binaryConsoleFile} mod --details")
                .ExecPayloadAsync<InstallerResponse<ModFeatureResponse>>(ct);
            if (modFeatureDetails.Error != default)
            {
                _crazyReport.ReportError($"{modFeatureDetails.Error.Code}=>{modFeatureDetails.Error.Message}");
                return default;
            }
            if (modFeatureDetails.Data == default)
                return default;
            return modFeatureDetails.Data.MapToDomain();
        }
        catch (JsonException ex)
        {
            _crazyReport.ReportErrorException(ex.Message, ex);
            throw new CouldNotReadModFeatureDetailsException();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
