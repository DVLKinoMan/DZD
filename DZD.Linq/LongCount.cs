using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static long LongCount<TSource>(this IEnumerable<TSource> source)
        {
            long count = 0;
            foreach (var s in source)
                count++;

            return count;
        }

        public static long LongCount<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            long count = 0;
            foreach (var s in source)
                if (predicate(s))
                    count++;

            return count;
        }

    }
}
