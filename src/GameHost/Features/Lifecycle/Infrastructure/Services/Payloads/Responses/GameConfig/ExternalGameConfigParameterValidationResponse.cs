namespace GameHost.Features.Lifecycle.Infrastructure.Services.Payloads.Responses.GameConfig;

public class ExternalGameConfigParameterValidationResponse
{
    public string Type { get; set; } = default!;
    public string Data { get; set; } = default!;
}
//{
//    "type": "lengthConstraint",
//    "data": {
//      "min": 10,
//      "max":  100
//    }
//  }