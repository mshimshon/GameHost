namespace GameHost.Features.Mods.Web.Components;

public partial class ModListSelector
{
    private const string NO_MODLIST_AVAILABLE_MSG = "No Mod is available to be select go create one to select a modlist."; // TODO: Localize
    private const string AWAITING_FOR_AVAILABLE_MODLIST = "Waiting for Available Modlist"; // TODO: Localize
    private const string AWAITING_FOR_CURRENT_MODLIST = "Fetching Current Modlist"; // TODO: Localize
    private const string UNSET_CURRENT = "Do not use mods"; // TODO: Localize

    private bool IsAvailableModListEmpty => ViewModel.ModListState.Available.Count <= 0 && !ViewModel.ModListState.IsLoadingAvailable;
    private Guid? GetValue()
        => ViewModel.CurrentModList == Guid.Empty ? default : (Guid?)ViewModel.CurrentModList;

    private Func<Guid?, Task> OnValueChanged => ValueChanged;
    private Task ValueChanged(Guid? id)
    {
        ViewModel.CurrentModList = id ?? Guid.Empty;

        return Task.CompletedTask;
    }
}
