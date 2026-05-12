using Microsoft.AspNetCore.Components;

namespace GameHost.Features.Mods.Web.Components;


public partial class DeleteButtonIcon<TEntity>
{

    public enum Stage
    {
        Initial,
        Confirm,
        Cancel,
        Busy,
        Deleted
    }

    [Parameter]
    public Func<TEntity, CancellationToken, Task<bool>> OnClick { get; set; } = default!;


    [Parameter]
    public Func<TEntity, CancellationToken, Task<bool>>? OnRestore { get; set; }

    [Parameter]
    public int ResetDelay { get; set; } = 800;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public TEntity Item { get; set; } = default!;

    [Parameter]
    public bool IsDeleted { get; set; }

    [Parameter]
    public Stage InitialStage { get; set; } = Stage.Initial;

    private Stage _stage = Stage.Initial;
    private CancellationTokenSource _waitCancelSource = new();
    private object _lockStage = new();

    protected override void OnWidgetInitialized()
    {
        _stage = InitialStage;
    }
    protected override void OnWidgetParametersSet()
    {
        if (_stage != Stage.Deleted && IsDeleted)
            _stage = Stage.Deleted;
    }

    private async Task InitialClick()
    {
        lock (_lockStage)
        {
            if (_stage != Stage.Initial) return;
            if (!_waitCancelSource.IsCancellationRequested)
                _waitCancelSource.Cancel();
            _stage = Stage.Confirm;
        }
        await InvokeAsync(StateHasChanged);
        _waitCancelSource = new();
        _ = Wait(ResetDelay, (ct) => _stage = Stage.Initial, _waitCancelSource.Token);
    }

    private async Task Confirm()
    {
        lock (_lockStage)
        {
            if (_stage != Stage.Confirm) return;
            if (!_waitCancelSource.IsCancellationRequested)
                _waitCancelSource.Cancel();
            _stage = Stage.Busy;

        }
        await InvokeAsync(StateHasChanged);

        var successful = await OnClick.Invoke(Item, _waitCancelSource.Token);
        if (OnRestore != default && successful)
        {
            _stage = Stage.Cancel;
        }
        else
            _stage = Stage.Deleted;

        await InvokeAsync(StateHasChanged);
    }

    private async Task Cancel()
    {
        if (OnRestore == default) return;
        lock (_lockStage)
        {
            if (_stage != Stage.Cancel) return;
            if (!_waitCancelSource.IsCancellationRequested)
                _waitCancelSource.Cancel();
            _stage = Stage.Busy;
        }
        await InvokeAsync(StateHasChanged);
        var successful = await OnRestore.Invoke(Item, _waitCancelSource.Token);
        if (successful)
        {
            _stage = Stage.Initial;
        }
        else
            _stage = Stage.Cancel;
        await InvokeAsync(StateHasChanged);
    }

    private async Task Wait(int delay, Action<CancellationToken> exec, CancellationToken ct = default)
    {
        await Task.Delay(delay, ct);
        lock (_lockStage)
        {
            if (ct.IsCancellationRequested) return;
            exec(ct);
        }
        if (ct.IsCancellationRequested) return;
        await InvokeAsync(StateHasChanged);
    }

}
