using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Domain.Entites;
using GameHost.Features.Lifecycle.Domain.Enums;
using GameHost.Features.Lifecycle.Web.Components.ViewModels;
using LunaticPanel.Core.Abstraction.Widgets;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Web.Components;

public class StartupParameterFieldViewModel : WidgetViewModelBase, IStartupParameterFieldViewModel
{



    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public bool IsTouched => InitialValue != Value;

    public GameConfigParamaterEntity Parameter { get; set; } = default!;

    public string Value { get; set; } = string.Empty;
    public string InitialValue { get; set; } = string.Empty;
    public bool HasValidation => Parameter.Validation != default;
    public bool IsList => Parameter.Validation?.AllowedValues != default && (IsListOfInt || IsListOfDecimal || IsListOfString);
    public bool IsListOfInt => Parameter.Key.ConfigParameterType == ConfigParameterType.List_Interger;
    public bool IsListOfDecimal => Parameter.Key.ConfigParameterType == ConfigParameterType.List_Decimal;
    public bool IsListOfString => Parameter.Key.ConfigParameterType == ConfigParameterType.List_String;
    public bool IsDecimal => Parameter.Key.ConfigParameterType == ConfigParameterType.Decimal;
    public bool IsInt => Parameter.Key.ConfigParameterType == ConfigParameterType.Int;
    public bool IsNumber => IsDecimal || IsInt;


    public StartupParameterFieldViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

    public async Task Save()
    {
        await _dispatcher.Prepare<UpdateStartupParameterAction>()
            .With(p => p.Key, Parameter.Key.Key)
            .With(p => p.Value, Value)
            .DispatchAsync();
    }

    public void Reset()
    {
        Value = InitialValue;
        _ = UpdateChanges();
    }
    public bool Validate()
    {

        return true;
    }

    public string GetLabel()
    {
        string defaultValue = InitialValue;
        if (string.IsNullOrWhiteSpace(defaultValue))
            return Parameter.Key.Key;
        return $"{Parameter.Key.Key} (Current: {defaultValue})";
    }
}
