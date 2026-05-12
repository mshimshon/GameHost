using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameHost.Features.Mods.Web.Components.Dialogs;

public partial class CreateModListDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    private MudTextField<string> _nameRef = default!;
    private Func<string, IEnumerable<string>> _nameValidator = default!;

    protected override void OnWidgetInitialized()
    {
        _nameValidator = NameValidator;
    }
    private IEnumerable<string> NameValidator(string name)
    {
        if (!ViewModel.IsNotNullPass(name))
            yield return "Name is required."; // TODO: Localize
        if (!ViewModel.IsMaxCharacterPass(name, 256))
            yield return "Max 256 characters"; // TODO: Localize
        if (!ViewModel.IsMinCharacterPass(name, 5))
            yield return "Min 5 characters"; // TODO: Localize
        if (!ViewModel.IsAlphaNumericAndSpacePass(name))
            yield return "Only A-Z, 0-9 and Space is allowed."; // TODO: Localize
    }

    protected override void OnWidgetParametersSet()
    {
        Console.WriteLine("Parameter Set");
        ViewModel.OnParameterSet();
    }
    protected override void OnWidgetBeforeRender()
    {
        ViewModel.OnParameterSet();
    }

    private async Task CompleteAsync()
    {
        MudDialog.Close(DialogResult.Ok(ViewModel.Id));
    }

    private async Task CancelAsync()
    {
        MudDialog.Cancel();
    }
}
