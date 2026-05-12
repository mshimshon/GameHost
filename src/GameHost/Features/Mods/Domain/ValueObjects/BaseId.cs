using GameHost.Features.Mods.Domain.Exceptions;
using GameHost.Features.Mods.Domain.Exceptions.Extensions;

namespace GameHost.Features.Mods.Domain.ValueObjects;

public abstract record BaseStringId
{
    public string Id { get; }
    protected BaseStringId(string id)
    {
        Id = id;
        ValidateId<BaseStringId>();
    }
    protected virtual void ValidateId<TOrigin>()
    {
        Id.ThrowIfNullOrWhiteSpace<TOrigin>(nameof(Id));
        if (Id.Any(char.IsWhiteSpace))
            throw new PartIdForbiddenCharactersException("Spaces");
    }
}

public abstract record BaseStringId<TOrigin> : BaseStringId
{
    protected BaseStringId(string id) : base(id)
    {
        Id.ThrowIfNullOrWhiteSpace<TOrigin>(nameof(Id));
    }

    protected sealed override void ValidateId<T>()
    {
        base.ValidateId<TOrigin>();
    }

}
