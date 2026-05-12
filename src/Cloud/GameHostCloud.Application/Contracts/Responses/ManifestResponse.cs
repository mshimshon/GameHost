namespace GameHostCloud.Application.Contracts.Responses;

public sealed record ManifestResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Filename { get; set; } = default!;
    public string Source { get; set; } = default!;
    public List<string> CompatibleDistro { get; set; } = default!;
    public string? Icon { get; set; }
}
