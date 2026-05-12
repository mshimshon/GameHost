using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Domain.ValueObjects;
using GameHost.Features.Mods.Web.Components.ViewModels;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Components;

internal sealed class ModListExplorerViewModel : WidgetViewModelBase, IModListExplorerViewModel
{
    private readonly IStatePulse _statePulse;
    public ModListLocalState ModListLocalState => _statePulse.StateOf<ModListLocalState>(() => this, UpdateChanges);
    public ModListState ModListState => _statePulse.StateOf<ModListState>(() => this, UpdateChanges);
    private IDispatcher Dispatcher => _statePulse.Dispatcher;
    public ModListDescriptor? Create { get; set; }
    public ModListExplorerViewModel(IStatePulse statePulse)
    {
        _statePulse = statePulse;
    }

    protected override async Task OnViewModelAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await GetAvailableAsync();
    }

    public async Task GetAvailableAsync()
    {
        var allowedNextRun = ModListState.LastCheck.AddSeconds(10);
        var preventRun = DateTime.UtcNow < allowedNextRun || ModListState.IsLoadingAvailable;
        if (preventRun) return;

        IsLoading = true;
        await Dispatcher.Prepare<GetAvailableModListAction>().DispatchAsync();
        IsLoading = false;
    }

    public async Task GetAsync(Guid id)
    {
        IsLoading = true;
        await Dispatcher.Prepare<GetModListAction>()
            .With(p => p.Id, id)
            .DispatchAsync();
        IsLoading = false;
    }

    protected override bool GetStateLoadingStatus() => ModListLocalState.IsCurrentLoading || ModListState.IsLoadingAvailable || !FirstRenderCompleted;

}
