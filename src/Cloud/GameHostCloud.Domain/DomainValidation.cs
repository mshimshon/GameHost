using GameHostCloud.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace GameHostCloud.Domain;

internal static class DomainValidation
{

    private static void ThrowException<TException>()
        where TException : notnull, DomainException, new()
    {
        var exception = Activator.CreateInstance<TException>();
        throw exception;
    }
    public static string PatternMatch<TException>(this string input, string pattern)
        where TException : notnull, DomainException, new()
    {
        if (!Regex.IsMatch(input, pattern))
            ThrowException<TException>();
        return input;
    }

    public static object NullThrow<TException>(this object input)
where TException : notnull, DomainException, new()
    {
        if (input is null) ThrowException<TException>();
        return input!;
    }

    public static string NullEmptyThrow<TException>(this string input)
        where TException : notnull, DomainException, new()
    {
        if (string.IsNullOrEmpty(input)) ThrowException<TException>();
        return input;
    }

    public static string NullWhiteSpaceThrow<TException>(this string input)
    where TException : notnull, DomainException, new()
    {
        if (string.IsNullOrWhiteSpace(input)) ThrowException<TException>();
        return input;
    }
}
