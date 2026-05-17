using GameHost.Core;
using GameHost.Core.Features;
using Microsoft.AspNetCore.Components;

namespace GameHost.Features.Mods.Web.Pages;

public partial class ModListHome
{
    [Parameter]
    public Guid? Id { get; set; }
    private const string BASE_LINK = $"{BaseInfo.ASSEMBLY_NAME}/{ModListKeys.MODULE_NAME}";
    protected override async Task OnWidgetParametersSetAsync()
    {
        if (Id == default) return;
        if (FirstRenderCompleted)
            await ViewModel.GetAsync((Guid)Id!);

    }
    protected override async Task OnWidgetAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (Id != default)
                await ViewModel.GetAsync((Guid)Id!);
        }
    }
}
