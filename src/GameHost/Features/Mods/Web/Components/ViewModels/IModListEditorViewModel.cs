using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Application.Pulses.States;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;

namespace GameHost.Features.Mods.Web.Components.ViewModels;

public interface IModListEditorViewModel : IWidgetViewModel
{
    Guid InitialId { get; set; }
    ModListLocalState ModListLocalState { get; }
    ModListState ModListState { get; }
    string ModListName { get; }
    Dictionary<PartId, List<ModEntity>>? Information { get; }
    string GetModName(ModEntity item);
    void MoveTo(PartId partId, ModEntity toMove, int targetIndex);
    void AddTo(PartId partId, ModEntity toAdd);
    void RemoveFrom(PartId partId, ModEntity toRemove);
    Task CloseAsync();
    Task SaveAsync();
    Task DeleteAsync();
}
