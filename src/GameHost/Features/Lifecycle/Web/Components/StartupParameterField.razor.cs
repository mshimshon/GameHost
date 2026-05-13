using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GameHost.Features.Lifecycle.Web.Components;

public partial class StartupParameterField
{
    [Parameter]
    public GameConfigParamaterEntity GameStartupParameter { get; set; } = default!;

    [Parameter]
    public string InitialValue { get; set; } = default!;

    public MudSelect<string>? MudSelectRef { get; set; }
    public MudNumericField<int>? MudNumericFieldIntRef { get; set; }
    public MudNumericField<double>? MudNumericFieldDecimalRef { get; set; }
    public MudTextField<string>? MudTextFieldRef { get; set; }
    public MudSwitch<bool>? MudSwitchRef { get; set; }

    protected override void OnWidgetParametersSet()
    {
        bool wasTouched = ViewModel.IsTouched;
        ViewModel.Parameter = GameStartupParameter;
        ViewModel.InitialValue = InitialValue;
        if (!wasTouched || string.Equals(ViewModel.Value, InitialValue, StringComparison.Ordinal))
            ViewModel.Value = InitialValue;
    }

    private int GetMaxLength() => ViewModel.Parameter.Validation?.MaxLength ?? 524288;


    private int ValueInt
    {
        get => !string.IsNullOrWhiteSpace(ViewModel.Value) ? int.Parse(ViewModel.Value) : 0;
        set { ViewModel.Value = value.ToString(); }
    }

    private double ValueDecimal
    {
        get => !string.IsNullOrWhiteSpace(ViewModel.Value) ? double.Parse(ViewModel.Value) : 0;
        set { ViewModel.Value = value.ToString(); }
    }

    private bool ValueBool
    {
        get => !string.IsNullOrWhiteSpace(ViewModel.Value) && bool.Parse(ViewModel.Value);
        set { ViewModel.Value = value.ToString(); }
    }


}
