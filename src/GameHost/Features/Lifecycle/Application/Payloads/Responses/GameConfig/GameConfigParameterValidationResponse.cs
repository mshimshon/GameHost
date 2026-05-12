namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig;

public class GameConfigParameterValidationResponse
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