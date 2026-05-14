using GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Lifecycle.Web.Components.ViewModels;

public interface IStartupParameterFieldViewModel : IWidgetViewModel
{
    GameConfigParameterResponse Parameter { get; set; }
    int MaxLength { get; }
    int MinLength { get; }
    string Value { get; set; }
    string InitialValue { get; set; }
    Dictionary<string, string> AllowedValues { get; }
    bool HasValidation { get; }
    bool IsList { get; }
    bool IsListOfString { get; }
    bool IsString { get; }
    bool IsBool { get; }
    bool IsListOfInt { get; }
    bool IsListOfDecimal { get; }
    bool IsDecimal { get; }
    bool IsInt { get; }
    bool IsNumber { get; }
    bool IsTouched { get; }
    Task Save();
    void Reset();
    bool Validate();
    string GetLabel();
}
