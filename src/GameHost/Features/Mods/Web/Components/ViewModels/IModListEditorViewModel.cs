using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Application.Pulses.States;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Mods.Web.Components.ViewModels;

public interface IModListEditorViewModel : IWidgetViewModel
{
    Guid InitialId { get; set; }
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    string ModListName { get; }
    Dictionary<string, List<ModResponse>>? Information { get; }
    string GetModName(ModResponse item);
    void MoveTo(string partId, ModResponse toMove, int targetIndex);
    void AddTo(string partId, ModResponse toAdd);
    void RemoveFrom(string partId, ModResponse toRemove);
    Task CloseAsync();
    Task SaveAsync();
    Task DeleteAsync();
}
