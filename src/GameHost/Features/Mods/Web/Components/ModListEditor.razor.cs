using GameHost.Features.Mods.Application.Payloads.Responses;
using GameHost.Features.Mods.Web.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameHost.Features.Mods.Web.Components;

public partial class ModListEditor
{



    private const string ERROR_SCHEMATIC_PARTS = "It Looks like the Mod Schematic had an error loading up."; // TODO: Localize
    private const string WARNING_FILE_CLOSED = "It Looks you have successfully closed the modlist file."; // TODO: Localize
    private const string BTN_CLOSE = "Close"; // TODO: Localize
    private const string BTN_SAVE = "Save & Close"; // TODO: Localize
    private const string BTN_DELETE = "Delete"; // TODO: Localize
    private const string BTN_FORCE_CLOSE = "Force Close"; // TODO: Localize
    [Inject] public IDialogService DialogService { get; set; } = default!;
    private void ItemUpdated(MudItemDropInfo<ModResponse> dropItem)
        => ViewModel.MoveTo(dropItem.DropzoneIdentifier, dropItem.Item!, dropItem.IndexInZone);
    private readonly DialogOptions _creationDialogOptions = new()
    {
        MaxWidth = MaxWidth.Medium,
        BackdropClick = false,
        CloseButton = true,
        CloseOnEscapeKey = true
    };
    private async Task AddInto(string partId)
    {
        var referenceDialog = await DialogService.ShowAsync<ModListEditorCreateModDialog>("Create Mod", _creationDialogOptions);
        var result = await referenceDialog.Result;
        if (result?.Canceled ?? true) return;
        if (result.Data == default) return;
        ModResponse toAdd = (ModResponse)result.Data;
        ViewModel.AddTo(partId, toAdd);

    }

    private async Task<bool> DeleteEntity(string partId, ModResponse toDelete, CancellationToken ct = default)
    {
        try
        {
            ViewModel.RemoveFrom(partId, toDelete);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
