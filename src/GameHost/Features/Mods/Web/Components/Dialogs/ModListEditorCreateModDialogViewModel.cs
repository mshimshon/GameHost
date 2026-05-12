using LunaticPanel.Core.Abstraction.Widgets;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;
using GameHost.Features.Mods.Web.Components.Dialogs.ViewModels;
using System.Text.RegularExpressions;

namespace GameHost.Features.Mods.Web.Components.Dialogs;

internal class ModListEditorCreateModDialogViewModel : WidgetViewModelBase, IModListEditorCreateModDialogViewModel
{
    private const string ALPHA_NUMERIC_N_SPACES = @"^[A-Za-z0-9 ]+$";
    public string? Id { get; set; }
    public string? Name { get; set; }
    public bool IsFormValid()
    {
        if (!IsNotNullPass(Id)) return false;
        if (string.IsNullOrEmpty(Name)) return true;

        return IsMaxCharacterPass(Name, 256) &&
               IsMinCharacterPass(Name, 2) &&
               IsAlphaNumericAndSpacePass(Name);
    }

    public bool IsNotNullPass(string? str) => !string.IsNullOrWhiteSpace(str);
    public bool IsMaxCharacterPass(string str, int maxLength) => str.Length <= maxLength;
    public bool IsMinCharacterPass(string str, int minLength) => str.Length >= minLength;
    public bool IsAlphaNumericAndSpacePass(string str) => Regex.IsMatch(str, ALPHA_NUMERIC_N_SPACES);

    public ModEntity GenerateResult()
    {
        var id = new ModId(Id!);
        var entity = new ModEntity(id)
        {
            Name = string.IsNullOrEmpty(Name) ? default : new ModName(Name)
        };
        return entity;
    }
}
