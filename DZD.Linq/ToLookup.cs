using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public static partial class Enumerable
    {
        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ToLookup(keySelector, element => element, EqualityComparer<TKey>.Default);
        }
        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return source.ToLookup(keySelector, element => element, comparer);
        }

        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return source.ToLookup(keySelector, elementSelector, EqualityComparer<TKey>.Default);
        }

        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (keySelector == null)
                throw new ArgumentNullException("keySelector");

            if (elementSelector == null)
                throw new ArgumentNullException("elementSelector");

            Lookup<TKey, TElement> lookUp = new Lookup<TKey, TElement>(comparer ?? EqualityComparer<TKey>.Default);

            foreach (var s in source)
                lookUp.Add(keySelector(s), elementSelector(s));

            return lookUp;
        }
    }
}
