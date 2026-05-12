namespace GameHost.Features.Mods.Domain.ValueObjects;

public sealed record PartId(string Id) : BaseStringId<PartId>(Id);
