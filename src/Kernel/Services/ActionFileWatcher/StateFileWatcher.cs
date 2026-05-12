using GameHost.Core.Features;
using GameHost.Kernel.Abstractions.Services.ActionFileWatcher;
using GameHost.Kernel.Abstractions.Services.ActionFileWatcher.Enums;
using LunaticPanel.Core.Utils.Abstraction.Logging;
using StatePulse.Net;
using System.Collections.Concurrent;

namespace GameHost.Kernel.Services.ActionFileWatcher;

internal class StateFileWatcher<TAction> : IStateFileWatcher<TAction> where TAction : FileWatchActionBase
{
    private bool _disposed;
    private readonly FileSystemWatcher _fileSystemWatcher;
    private readonly IDispatcher _dispatcher;
    private readonly ICrazyReport _crazyReport;
    private readonly FileWatchEvents[] _whatToWatch;
    public string Directory { get; init; }
    public string FilePattern { get; init; }

    private readonly string _path;
    private readonly ConcurrentQueue<StateFileNotifyQueueElement> _changeQueue = new();
    private readonly Task _queueWorker;
    private readonly CancellationTokenSource _cts = new();

    private readonly ConcurrentDictionary<FileTrackerKey, long> _pendingByPath = new();
    public StateFileWatcher(string path, string filePattern, FileWatchEvents[] whatToWatch, IDispatcher dispatcher, ICrazyReport<TAction> crazyReport)
    {

        Directory = path;
        FilePattern = filePattern;
        _dispatcher = dispatcher;
        _crazyReport = crazyReport;
        _crazyReport.SetModule(KernelKeys.MODULE_NAME);
        _whatToWatch = whatToWatch;
        var linuxWorkAroundFileName = Path.Combine(path, Path.GetRandomFileName());
        _crazyReport.ReportInfo("Creating TMP Fix Workaround: {0}", linuxWorkAroundFileName);
        File.WriteAllText(linuxWorkAroundFileName, "");
        _fileSystemWatcher = new FileSystemWatcher(path, filePattern);
        _fileSystemWatcher!.EnableRaisingEvents = true;
        File.Delete(linuxWorkAroundFileName);

        _fileSystemWatcher.Created += DispatchNotify;
        _fileSystemWatcher.Changed += DispatchNotify;
        _fileSystemWatcher.Deleted += DispatchNotify;
        _fileSystemWatcher.Renamed += DispatchNotify;
        _path = Path.Combine(path, filePattern);
        _queueWorker = Task.Run(() => WatchQueue(_cts.Token));
        _crazyReport.ReportInfo("Watching file {0} with following changes: {1}", _path, string.Join(", ", whatToWatch));
    }


    private void DispatchNotify(object _, FileSystemEventArgs e)
    {

        _crazyReport.ReportInfo("Attempt to Enqueue ({1}) for {0}", _path, e.ChangeType);
        FileWatchEvents eventTypeToWatch = e.ChangeType switch
        {
            WatcherChangeTypes.Created => FileWatchEvents.Created,
            WatcherChangeTypes.Deleted => FileWatchEvents.Removed,
            WatcherChangeTypes.Changed => FileWatchEvents.Updated,
            WatcherChangeTypes.Renamed => FileWatchEvents.Renamed,
            _ => FileWatchEvents.Any
        };

        FileWatchEvents eventType = eventTypeToWatch;
        bool isWatchingAnything = _whatToWatch.Contains(FileWatchEvents.Any);
        bool isWatchingTheEvent = isWatchingAnything || eventType == FileWatchEvents.Any ? isWatchingAnything : _whatToWatch.Contains(eventType);
        if (!isWatchingTheEvent)
        {
            _crazyReport.ReportWarning("isWatchingAnything {0};  isWatchingTheEvent {1};", isWatchingAnything, isWatchingTheEvent);
            _crazyReport.ReportWarning("Ignored {0} for {1} ", eventType, _path);
            return;
        }
        var version = _pendingByPath.AddOrUpdate(
            new(e.FullPath, eventType),
            _ => 1,
            (_, v) => v + 1
        );

        var action = new StateFileNotifyQueueElement()
        {
            Version = version,
            EventType = eventType,
            Path = e.FullPath,
            Action = () => Notify(e, eventType)
        };
        _crazyReport.Report("Enqueue {0}", action.ToString());
        Enqueue(action);


    }
    private async Task Notify(FileSystemEventArgs args, FileWatchEvents eventType)
    {

        TAction action = Activator.CreateInstance<TAction>();
        if (action == default) return;
        action.Event = eventType;
        action.FullName = args.FullPath;
        action.Date = DateTime.UtcNow;
        action.FileName = args.Name!;
        try
        {
            _crazyReport.Report("Running Update {0}", action.ToString());
            await _dispatcher.Prepared(action).Await().DispatchAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _crazyReport.ReportError("Error Running {0} with Exception {1}", action.ToString(), ex.Message);
        }
        //_ = Task.Run(async () =>
        //{
        //    try
        //    {
        //        _crazyReport.ReportInfo("Running Update {0}", action.ToString());
        //        await _dispatcher.Prepared(action).DispatchAsync().ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _crazyReport.ReportError("Error Running {0} with Exception {1}", action.ToString(), ex.Message);
        //    }
        //});

    }
    private readonly SemaphoreSlim _signal = new(0);

    private void Enqueue(StateFileNotifyQueueElement work)
    {
        _changeQueue.Enqueue(work);
        _signal.Release();
    }

    private async Task WatchQueue(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await _signal.WaitAsync(ct).ConfigureAwait(false);

            if (_changeQueue.TryDequeue(out var nextAction))
            {
                var key = new FileTrackerKey(nextAction.Path, nextAction.EventType);
                try
                {
                    // only keep lastest update and kill any stale
                    //_pendingByPath.TryGetValue(next.Path, )
                    _pendingByPath.TryGetValue(key, out long currentVersion);
                    bool isStaleAction = currentVersion == default || currentVersion > nextAction.Version;
                    _crazyReport.Report("Is Queue Action Stale? {0}", isStaleAction);
                    if (isStaleAction) continue;
                    await nextAction.Action().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _crazyReport.ReportError("WatchQueue Error {0}", ex.Message);
                    // TODO: Handle that shit
                }
                finally
                {
                    _pendingByPath.TryGetValue(key, out var currentVersion);
                    if (currentVersion != default && currentVersion == nextAction.Version)
                    {
                        if (_pendingByPath.TryRemove(key, out _))
                            _crazyReport.Report("WatchQueue Removing Version Tracking for {0} ({1} == {2})", nextAction.Path, currentVersion, nextAction.Version);

                    }
                }
            }


        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _cts.Cancel();
            _fileSystemWatcher.Dispose();
        }

        // free unmanaged resources here

        _disposed = true;
    }


}
