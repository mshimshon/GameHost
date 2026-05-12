namespace GameHost.Features.Mods.Domain.Exceptions.Extensions;

internal static class ExceptionExt
{
    public static void ThrowIfNullOrWhiteSpace<TCurrentClass>(this string str, string propName)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new EmptyException<TCurrentClass>(propName);
    }




}
