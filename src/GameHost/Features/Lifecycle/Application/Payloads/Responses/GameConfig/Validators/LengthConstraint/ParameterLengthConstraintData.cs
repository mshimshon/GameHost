namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators.LengthConstraint;

public sealed record ParameterLengthConstraintData
{
    public int Max { get; set; } = int.MaxValue;
    public int Min { get; set; } = int.MinValue;
}
