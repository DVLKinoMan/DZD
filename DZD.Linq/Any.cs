using System;
using System.Collections.Generic;

namespace DZD.Linq
{
    public partial class Enumerable
    {
        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            //Jon Skeet's version:
            //using (IEnumerator<TSource> iterator = source.GetEnumerator())
            //{
            //    return iterator.MoveNext();
            //}

            return source.Count() > 0;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (var s in source)
                if (predicate(s))
                    return true;

            return false;
        }
    }
}
