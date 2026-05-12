using GameHost.Features.Mods.Application.Contracts.Responses;
using GameHost.Features.Mods.Domain.Entities;
using GameHost.Features.Mods.Domain.ValueObjects;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.States;

public sealed record ModListState : IStateFeatureSingleton
{
    public ModFeatureResponse? FeatureInfo { get; init; }
    public bool IsFeatureInfoLoading { get; init; }
    public IReadOnlyCollection<ModListDescriptor> Available { get; init; } = Array.Empty<ModListDescriptor>().AsReadOnly();
    public bool IsLoadingAvailable { get; init; }
    public DateTime LastCheck { get; init; }
    public IReadOnlyCollection<PartSchematicEntity> SchematicParts { get; init; } = Array.Empty<PartSchematicEntity>().AsReadOnly();
    public bool IsSchematicPartsLoaded { get; init; }
    public bool IsSchematicPartsLoading { get; init; }

    /// <summary>
    /// Currently Active ModList select for the server to use at startup.
    /// </summary>
    public ModListDescriptor? Active { get; init; }
    public bool IsActiveLoading { get; init; }


}
