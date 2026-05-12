using GameHost.Core.Features;
using GameHost.Features.SystemInfo.Application.Services;
using GameHost.Features.SystemInfo.Domain.Entites;
using GameHost.Features.SystemInfo.Domain.ValueObjects;
using GameHost.Features.SystemInfo.Infrastructure.Configurations;
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

    public async Task<SystemInfoEntity?> GetSystemInfoAsync(CancellationToken ct = default)
    {
        var ramScript = _pluginSystemLocation.GetBashFor(SystemInfoKeys.MODULE_NAME, "get_ram_info.sh");
        var ram = await _linuxCommand.BuildBash(ramScript).ExecAsync(ct);
        _crazyReport.Report("Ram={0}", ram);
        var ramUsage = float.Parse(ram.StandardOutput.Split(';')[0]);
        var ramTotal = float.Parse(ram.StandardOutput.Split(';')[1]);

        var diskScript = _pluginSystemLocation.GetBashFor(SystemInfoKeys.MODULE_NAME, "get_disk_info.sh", _linuxSystemInfoConfiguration.WorkingDisk);
        var disk = await _linuxCommand.BuildBash(diskScript).ExecOutputAsync<string>(ct);
        _crazyReport.Report("Disk={0}", disk);
        var diskUsage = float.Parse(disk.Split(';')[0]);
        var diskTotal = float.Parse(disk.Split(';')[1]);

        var processorScript = _pluginSystemLocation.GetBashFor(SystemInfoKeys.MODULE_NAME, "get_cpu_info.sh");
        var processor = await _linuxCommand.BuildBash(processorScript).ExecOutputAsync<string>(ct);
        _crazyReport.Report("Processor={0}", processor);
        var processorUsage = float.Parse(processor.Split(';')[0]);
        var processorCores = int.Parse(processor.Split(';')[1]);
        var processorModel = processor.Split(';')[2];

        SystemInfoEntity? result = default;
        if (ram != default && disk != default && processor != default)
            result = new SystemInfoEntity()
            {
                Disk = new(diskUsage, diskTotal),
                Memory = new(ramUsage, ramTotal),
                Processor = new SystemProcessor(processorUsage, processorCores, processorModel)
            };

        return result;
    }

}
