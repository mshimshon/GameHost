using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.States;

namespace GameHost.Features.Mods.Web.Components.ViewModels;

public interface IModListSelectorViewModel : IWidgetViewModel
{
    Guid CurrentModList { get; set; }
    ModListState ModListState { get; }

    Task SaveAsync();

}
