using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.Actions;
using GameHost.Features.Mods.Web.Pages.ViewModels;
using StatePulse.Net;

namespace GameHost.Features.Mods.Web.Pages;

internal class ModListHomeViewModel : WidgetViewModelBase, IModListHomeViewModel
{
    private readonly IDispatcher _dispatcher;

    public Guid InitialId { get; set; }
    public ModListHomeViewModel(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    public async Task GetAsync(Guid id)
    {
        await _dispatcher.Prepare<GetModListAction>()
            .With(p => p.Id, id)
            .DispatchAsync();
    }
}
