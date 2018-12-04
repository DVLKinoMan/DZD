using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> func)
        {
            foreach (var s in source)
                foreach (var s2 in func(s))
                    yield return s2;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> func)
        {
            int index = 0;
            foreach (var s in source)
                foreach (var s2 in func(s, index++))
                    yield return s2;
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            int index = 0;
            foreach (var s in source)
                foreach (var s2 in collectionSelector(s, index++))
                    yield return resultSelector(s, s2);
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (var s in source)
                foreach (var s2 in collectionSelector(s))
                    yield return resultSelector(s, s2);
        }
    }
}
