using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameHost.Features.Mods.Web.Components.Dialogs;

public partial class ModListEditorCreateModDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;
    private Func<string, IEnumerable<string>> _idValidator = default!;
    private Func<string, IEnumerable<string>> _nameValidator = default!;

    protected override void OnWidgetInitialized()
    {
        _idValidator = IdValidator;
        _nameValidator = NameValidator;
    }
    private IEnumerable<string> IdValidator(string id)
    {
        if (!ViewModel.IsNotNullPass(id))
            yield return "The Id is Required."; // TODO: Localize
    }

    private IEnumerable<string> NameValidator(string displayName)
    {
        if (!string.IsNullOrEmpty(displayName))
        {
            if (!ViewModel.IsNotNullPass(displayName))
                yield return "Name cannot be whitespace."; // TODO: Localize
            if (!ViewModel.IsMaxCharacterPass(displayName, 256))
                yield return "Max 256 characters"; // TODO: Localize
            if (!ViewModel.IsMinCharacterPass(displayName, 2))
                yield return "Min 2 characters"; // TODO: Localize
            if (!ViewModel.IsAlphaNumericAndSpacePass(displayName))
                yield return "Only A-Z, 0-9 and Space is allowed."; // TODO: Localize
        }

    }

    private Task CancelAsync()
    {
        MudDialog.Close();
        return Task.CompletedTask;
    }

    private Task CompleteAsync()
    {
        MudDialog.Close(ViewModel.GenerateResult());
        return Task.CompletedTask;
    }
}
