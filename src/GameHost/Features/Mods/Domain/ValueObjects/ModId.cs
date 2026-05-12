namespace GameHost.Features.Mods.Domain.ValueObjects;

public sealed record ModId(string Id) : BaseStringId<ModId>(Id);
