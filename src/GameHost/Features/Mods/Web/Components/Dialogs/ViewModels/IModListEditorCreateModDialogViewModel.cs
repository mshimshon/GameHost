using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Domain.Entities;

namespace GameHost.Features.Mods.Web.Components.Dialogs.ViewModels;

public interface IModListEditorCreateModDialogViewModel : IWidgetViewModel
{
    string? Id { get; set; }
    string? Name { get; set; }
    bool IsFormValid();
    bool IsNotNullPass(string? str);
    bool IsMaxCharacterPass(string str, int maxLength);
    bool IsMinCharacterPass(string str, int minLength);
    bool IsAlphaNumericAndSpacePass(string str);
    ModEntity GenerateResult();
}
