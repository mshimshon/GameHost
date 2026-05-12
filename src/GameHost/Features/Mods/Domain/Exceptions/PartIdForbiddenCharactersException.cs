namespace GameHost.Features.Mods.Domain.Exceptions;

public class PartIdForbiddenCharactersException : DomainException
{
    public PartIdForbiddenCharactersException(string detectedChar) : base($"Id contains forbiden characters ({detectedChar})")
    {
    }
}
