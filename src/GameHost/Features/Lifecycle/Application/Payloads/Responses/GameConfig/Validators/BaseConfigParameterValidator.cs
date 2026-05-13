using System.Text.Json.Serialization;

namespace GameHost.Features.Lifecycle.Application.Payloads.Responses.GameConfig.Validators;


public abstract record BaseConfigParameterValidator
{
    public string Type { get; set; } = default!;
    [JsonIgnore]
    public object Data => GetAsGenericObject();
    public abstract string? Validate(params object[] data);
    protected abstract object GetAsGenericObject();

}
