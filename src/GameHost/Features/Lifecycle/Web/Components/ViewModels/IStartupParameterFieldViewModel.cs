using GameHost.Features.Lifecycle.Domain.Entites;
using LunaticPanel.Core.Abstraction.Widgets;

namespace GameHost.Features.Lifecycle.Web.Components.ViewModels;

public interface IStartupParameterFieldViewModel : IWidgetViewModel
{
    GameConfigParamaterEntity Parameter { get; set; }
    string Value { get; set; }
    string InitialValue { get; set; }
    bool HasValidation { get; }
    bool IsList { get; }
    bool IsListOfString { get; }
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
