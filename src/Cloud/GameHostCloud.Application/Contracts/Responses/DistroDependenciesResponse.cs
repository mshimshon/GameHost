namespace GameHostCloud.Application.Contracts.Responses;

public class DistroDependenciesResponse
{
    public int Distro { get; }
    public List<string> Common { get; set; } = default!;
    public Dictionary<string, List<string>> Specific { get; set; } = default!;
}
