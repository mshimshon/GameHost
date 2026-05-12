using System.Text.Json.Serialization;

namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

internal class GameInfoResponse
{
    public bool Modding { get; set; }
    public bool ManualModUpload { get; set; }
    [JsonPropertyName("mod_schema")]
    public Dictionary<string, ModSchematicResponse>? Schema { get; set; }
}
