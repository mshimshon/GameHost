using MudBlazor;

namespace GameHost.Features.Mods.Web.Components;

public partial class ButtonLoader
{
    public List<string> IconCycle { get; set; } = new()
    {
        Icons.Material.Filled.Sync,
        Icons.Material.Filled.Autorenew,
        Icons.Material.Filled.Cached,

    };
    private CancellationTokenSource _loopCts = default!;
    private Queue<string> _cycles = new();
    private string _currentIcon = default!;
    protected override void OnWidgetInitialized()
    {
        _currentIcon = IconCycle.Last();
        foreach (var item in IconCycle)
            _cycles.Enqueue(item);

    }
    protected override Task OnWidgetAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _loopCts = new();
            _ = Flipper(_loopCts.Token);
        }
        return Task.CompletedTask;

    }
    private async Task Flipper(CancellationToken ct = default)
    {
        do
        {
            _currentIcon = _cycles.Dequeue();
            _cycles.Enqueue(_currentIcon);
            await InvokeAsync(StateHasChanged);
            await Task.Delay(125);

        } while (!_loopCts.IsCancellationRequested);
    }
    protected override void OnWidgetDispose()
    {
        if (!_loopCts.IsCancellationRequested)
            _loopCts.Cancel();
    }
}
