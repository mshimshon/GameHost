namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.AllowedValues;

public sealed record ParameterAllowedValuesValidator : BaseConfigParameterValidator
{
    public new List<ParameterAllowedValuesData> Data { get; set; } = default!;
    public override string? Validate(params object[] data) => throw new NotImplementedException();
    protected override object GetAsGenericObject() => Data;
}
