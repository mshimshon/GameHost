namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.LengthConstraint;


public sealed record ParameterLengthConstraintValidator : BaseConfigParameterValidator
{
    public new ParameterLengthConstraintData Data { get; set; } = default!;

    public override string? Validate(params object[] data) => throw new NotImplementedException();
    protected override object GetAsGenericObject() => Data;
}

