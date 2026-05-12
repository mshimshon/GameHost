namespace GameHost.Features.Mods.Domain.Exceptions;

public class EmptyException<TObject> : DomainException
{
    public EmptyException(string field) : base($"The {field} in {typeof(TObject).Name} cannot be empty or null")
    {
    }
}
