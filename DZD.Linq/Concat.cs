using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            return ConcatImpl(first, second);
        }

        private static IEnumerable<TSource> ConcatImpl<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var s in first)
                yield return s;

            foreach (var s2 in second)
                yield return s2;
        }
    }
}
