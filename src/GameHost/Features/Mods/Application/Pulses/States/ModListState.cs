using GameHost.Features.Mods.Application.Payloads.Responses;
using StatePulse.Net;

namespace GameHost.Features.Mods.Application.Pulses.States;

public sealed record ModListState : IStateFeatureSingleton
{
    public ModFeatureResponse? FeatureInfo { get; init; }
    public bool IsFeatureInfoLoading { get; init; }
    public IReadOnlyCollection<ModListDescriptorResponse> Available { get; init; } = Array.Empty<ModListDescriptorResponse>().AsReadOnly();
    public bool IsLoadingAvailable { get; init; }
    public DateTime LastCheck { get; init; }
    public IReadOnlyCollection<PartSchematicResponse> SchematicParts { get; init; } = Array.Empty<PartSchematicResponse>().AsReadOnly();
    public bool IsSchematicPartsLoaded { get; init; }
    public bool IsSchematicPartsLoading { get; init; }

    /// <summary>
    /// Currently Active ModList select for the server to use at startup.
    /// </summary>
    public ModListDescriptorResponse? Active { get; init; }
    public bool IsActiveLoading { get; init; }


}
