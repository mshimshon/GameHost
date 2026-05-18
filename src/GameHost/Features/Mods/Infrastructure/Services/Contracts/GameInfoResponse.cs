namespace GameHost.Features.Mods.Infrastructure.Services.Contracts;

internal class GameInfoResponse
{
    public bool Modding { get; set; }
    public bool ManualModUpload { get; set; }
    public Dictionary<string, ModSchematicResponse>? ModSchema { get; set; }
}
