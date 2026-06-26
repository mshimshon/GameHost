using GameHost.Core;
using GameHost.Core.Features;
using GameHost.Features.Mods.Web.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameHost.Features.Mods.Web.Components;

public partial class ModListExplorer
{
    [Parameter]
    public bool AutoRedirect { get; set; } = false;

    [Parameter]
    public string LinkBaseNoTrailing { get; set; } = $"/{GameHostKeys.ASSEMBLY_NAME}/{ModListKeys.MODULE_NAME}";

    [Inject] public NavigationManager Navigation { get; set; } = default!;
    [Inject] public IDialogService DialogService { get; set; } = default!;

    private async Task OpenModList(Guid id)
    {
        if (AutoRedirect)
            Navigation.NavigateTo($"{LinkBaseNoTrailing}/{id}");
        else
            await ViewModel.GetAsync(id);
    }

    private readonly DialogOptions _creationDialogOptions = new()
    {
        MaxWidth = MaxWidth.Medium,
        BackdropClick = false,
        CloseButton = true,
        CloseOnEscapeKey = true
    };
    private async Task CreateAsync()
    {
        var dialog = await DialogService.ShowAsync<CreateModListDialog>("Create a new ModList.", _creationDialogOptions);
        var result = await dialog.Result;

    }
}
