using GameHost.Kernel.Abstractions.Extensions;

namespace GameHost.Kernel.Abstractions.Extensions;

public static class LinqConversionExt
{
    public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> ToReadOnlyDictionaryWithReadOnlyCollectionValue<TKey, TValue>(this Dictionary<TKey, List<TValue>> input)
        where TKey : notnull
        where TValue : notnull
        => input.ToDictionary(
            p => p.Key,
            p => (IReadOnlyCollection<TValue>)p.Value.AsReadOnly())
            .AsReadOnly();

    public static IReadOnlyDictionary<TKey, IReadOnlyList<TValue>> ToReadOnlyDictionaryWithReadOnlyListValue<TKey, TValue>(this Dictionary<TKey, List<TValue>> input)
        where TKey : notnull
        where TValue : notnull
        => input.ToDictionary(
            p => p.Key,
            p => (IReadOnlyList<TValue>)p.Value.AsReadOnly())
            .AsReadOnly();


    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, IReadOnlyList<TValue>> input)
        where TKey : notnull
        where TValue : notnull
        => input.ToDictionary(
            p => p.Key,
            p => p.Value.ToList());
    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, IReadOnlyList<TValue>>> input)
        where TKey : notnull
        where TValue : notnull
        => input.ToDictionary(
            p => p.Key,
            p => p.Value.ToList());



    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> input)
        where TKey : notnull
        where TValue : notnull
        => input.ToDictionary(
            p => p.Key,
            p => p.Value.ToList());

    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> input)
    where TKey : notnull
    where TValue : notnull
    => input.ToDictionary(
        p => p.Key,
        p => p.Value.ToList());
}