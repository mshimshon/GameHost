using GameHostCloud.Domain.Entities.Enums;
using GameHostCloud.Domain.ValueObjects;

namespace GameHostCloud.Domain.Entities;

public sealed record DistroDependenciesEntity
{
    public Distro Distro { get; }
    public IReadOnlyCollection<string> Common { get; }
    public IReadOnlyCollection<SpecificDependencies> SpecificDependencies { get; }
    public DistroDependenciesEntity(Distro distro, ICollection<string> common, ICollection<SpecificDependencies> specificDependencies)
    {
        Common = common.ToList().AsReadOnly();
        SpecificDependencies = specificDependencies.ToList().AsReadOnly();
        Distro = distro;
    }
}
