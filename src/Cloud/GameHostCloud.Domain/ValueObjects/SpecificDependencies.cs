namespace GameHostCloud.Domain.ValueObjects;

public sealed record SpecificDependencies
{
    public string Id { get; }
    public IReadOnlyCollection<string> Dependencies { get; }
    public SpecificDependencies(string id, ICollection<string> dependencies)
    {
        Id = id;
        Dependencies = dependencies.ToList().AsReadOnly();
    }
}
