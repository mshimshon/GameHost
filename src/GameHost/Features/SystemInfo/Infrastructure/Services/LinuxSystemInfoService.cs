using GameHost.Core.Features;
using GameHost.Features.SystemInfo.Application.Services;
using GameHost.Features.SystemInfo.Domain.Entites;
using GameHost.Features.SystemInfo.Infrastructure.Configurations;
using GameHost.Features.SystemInfo.Infrastructure.Payloads.Response;
using GameHost.Features.SystemInfo.Infrastructure.Payloads.Response.Mapping;
using LunaticPanel.Core.Utils.Abstraction.LinuxCommand;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using LunaticPanel.Core.Utils.Abstraction.Plugin.Location;

namespace GameHost.Features.SystemInfo.Infrastructure.Services;

internal class LinuxSystemInfoService : ISystemInfoService
{
    private readonly ILinuxCommand _linuxCommand;
    private readonly LinuxSystemInfoConfiguration _linuxSystemInfoConfiguration;
    private readonly IPluginSystemLocation _pluginSystemLocation;
    private readonly ICrazyReport<LinuxSystemInfoService> _crazyReport;
    public LinuxSystemInfoService(ILinuxCommand linuxCommand, LinuxSystemInfoConfiguration linuxSystemInfoConfiguration,
        IPluginLocation pluginLocation,
        ICrazyReport<LinuxSystemInfoService> crazyReport)
    {
        _linuxCommand = linuxCommand;
        _linuxSystemInfoConfiguration = linuxSystemInfoConfiguration;
        _pluginSystemLocation = pluginLocation;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(SystemInfoKeys.MODULE_NAME);
    }
    private async Task<SystemInfoRamResponse?> GetRamAsync(CancellationToken ct = default)
    {
        SystemInfoRamResponse? ramInfo = default;
        try
        {
            var ram = await _linuxCommand
                .BuildCommand("free -b")
                .PatchInStdPipeCommand("awk '/Mem:/ {printf \"%.2f;%.2f\", $3/1024/1024, $2/1024/1024}'")
                .PatchInStdOutAsPayload()
                .ExecPayloadAsync<string>(ct);
            var ramSplit = ram.Trim().Split(';');
            var ramUsage = float.Parse(ramSplit[0]);
            var ramTotal = float.Parse(ramSplit[1]);
            ramInfo = new()
            {
                Total = ramTotal,
                Current = ramUsage
            };
        }
        catch (Exception ex)
        {
            _crazyReport.ReportErrorException(ex.Message, ex);
        }
        return ramInfo;
    }
    private async Task<SystemInfoDiskResponse?> GetDiskAsync(CancellationToken ct = default)
    {
        SystemInfoDiskResponse? info = default; ;
        try
        {
            var ram = await _linuxCommand
                .BuildCommand($"df -B1 \"{_linuxSystemInfoConfiguration.WorkingDisk}\"")
                .PatchInStdPipeCommand("awk 'NR==2 {printf \"%.2f;%.2f\\n\", $3/1024/1024, $2/1024/1024}'")
                .PatchInStdOutAsPayload()
                .ExecPayloadAsync<string>(ct);
            var split = ram.Trim().Split(';');
            var usage = float.Parse(split[0]);
            var total = float.Parse(split[1]);
            info = new()
            {
                Total = total,
                Current = usage
            };
        }
        catch (Exception ex)
        {
            _crazyReport.ReportErrorException(ex.Message, ex);
        }
        return info;
    }
    private async Task<SystemInfoProcessorResponse?> GetProcessorAsync(CancellationToken ct = default)
    {
        SystemInfoProcessorResponse? info = default;
        try
        {
            var ram = await _linuxCommand
                .BuildCommand($"cpu_usage=$(grep 'cpu ' /proc/stat | awk '{{idle=$5; total=0; for(i=2;i<=NF;i++) total+=$i; print 100*(1-idle/total)}}')")
                .AndCommand("cores=$(nproc)")
                .AndCommand("model=$(awk -F': ' '/model name/ {print $2; exit}' /proc/cpuinfo)")
                .AndCommand("echo \"${cpu_usage};${cores};${model}\"")
                .PatchInStdOutAsPayload()
                .ExecPayloadAsync<string>(ct);
            var split = ram.Trim().Split(';');
            var usage = float.Parse(split[0]);
            var cores = int.Parse(split[1]);
            var model = split[2];
            info = new()
            {
                Cores = cores,
                Model = model,
                Current = usage
            };
        }
        catch (Exception ex)
        {
            _crazyReport.ReportErrorException(ex.Message, ex);
        }
        return info;
    }
    public async Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
    {

        var ram = await GetRamAsync(ct);
        var disk = await GetDiskAsync(ct);
        var processor = await GetProcessorAsync(ct);
        SystemInfoEntity? result = default;
        if (ram != default && disk != default && processor != default)
            result = new SystemInfoEntity()
            {
                Disk = disk.MapToDomain(),
                Memory = ram.MapToDomain(),
                Processor = processor.MapToDomain()
            };

        return result;
    }

}
