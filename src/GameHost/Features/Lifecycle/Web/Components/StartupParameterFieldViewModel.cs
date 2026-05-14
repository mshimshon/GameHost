using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Enums;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.AllowedValues;
using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.LengthConstraint;
using GameHost.Features.Lifecycle.Application.Pulses.Actions;
using GameHost.Features.Lifecycle.Web.Components.ViewModels;
using LunaticPanel.Core.Abstraction.Widgets;
using StatePulse.Net;

namespace GameHost.Features.Lifecycle.Web.Components;

public class StartupParameterFieldViewModel : WidgetViewModelBase, IStartupParameterFieldViewModel
{



    private readonly IStatePulse _statePulse;
    private readonly IDispatcher _dispatcher;

    public bool IsTouched => InitialValue != Value;

    public GameConfigParameterResponse Parameter { get; set; } = default!;

    public string Value { get; set; } = string.Empty;
    public string InitialValue { get; set; } = string.Empty;
    public bool HasValidation => Parameter.Validations?.Count > 0;
    public bool IsList => (IsListOfInt || IsListOfDecimal || IsListOfString);
    public bool IsListOfInt => Parameter.Type == ConfigParameterType.List_Int;
    public bool IsListOfDecimal => Parameter.Type == ConfigParameterType.List_Decimal;
    public bool IsListOfString => Parameter.Type == ConfigParameterType.List_String;
    public bool IsDecimal => Parameter.Type == ConfigParameterType.Decimal;
    public bool IsInt => Parameter.Type == ConfigParameterType.Int;

    public bool IsString => Parameter.Type == ConfigParameterType.String;

    public bool IsBool => Parameter.Type == ConfigParameterType.Bool || Parameter.Type == ConfigParameterType.Bool_Explicit ||
        Parameter.Type == ConfigParameterType.Bool_String;

    public bool IsNumber => IsDecimal || IsInt;
    public ParameterLengthConstraintValidator? LengthConstraintValidator { get; set; }
    public ParameterAllowedValuesValidator? AllowedValueValidator { get; set; }
    public int MaxLength => LengthConstraintValidator?.Data.Max ?? int.MaxValue;
    public int MinLength => LengthConstraintValidator?.Data.Min ?? int.MinValue;
    public Dictionary<string, string> AllowedValues { get; set; } = new();


    public StartupParameterFieldViewModel(IStatePulse statePulse, IDispatcher dispatcher)
    {
        _statePulse = statePulse;
        _dispatcher = dispatcher;
    }

    protected override void OnViewModelBeforeRender()
    {

    }

    public async Task Save()
    {
        await _dispatcher.Prepare<UpdateStartupParameterAction>()
            .With(p => p.Key, Parameter.Key)
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
    protected override void OnViewModelInitialized()
    {
        if (Parameter.Validations == default) return;
        foreach (var validation in Parameter.Validations)
            if (validation.Type == ValidatorDefinitions.LENGTH_CONSTRAINT)
                LengthConstraintValidator = (ParameterLengthConstraintValidator)validation.Data;
            else if (validation.Type == ValidatorDefinitions.LENGTH_CONSTRAINT)
                AllowedValueValidator = (ParameterAllowedValuesValidator)validation.Data;

        ProcessValidators();
    }


    private void ProcessValidators()
    {
        AllowedValues = AllowedValueValidator?.Data.ToDictionary(p => p.Value, p => p.Label) ?? new();
    }

    public string GetLabel()
    {
        string defaultValue = InitialValue;
        if (string.IsNullOrWhiteSpace(defaultValue))
            return Parameter.Key;
        return $"{Parameter.Key} (Current: {defaultValue})";
    }
}
