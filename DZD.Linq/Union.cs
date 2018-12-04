using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return first.Union(second, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (second == null)
                throw new ArgumentNullException("second");

            return first.UnionImpl(second, comparer ?? EqualityComparer<TSource>.Default);
            //Jon Skeet said that you can do this too:
            //return first.Concat(second).Distinct(comparer ?? EqualityComparer<TSource>.Default);
        }

        private static IEnumerable<TSource> UnionImpl<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> set = new HashSet<TSource>(comparer);

            foreach (var f in first)
                if (set.Add(f))
                    yield return f;

            foreach (var s in second)
                if (set.Add(s))
                    yield return s;
        }
    }
}
